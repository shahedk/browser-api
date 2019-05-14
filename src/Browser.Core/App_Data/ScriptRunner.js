// References
var system = require('system');
var page = require('webpage').create();
var fs = require('fs');

var parseResult = {
    "data": [],
    "errors": [],
    "scripts": [],
    "status": "in progress",
    "screenShotFileName": "",
    "rawHtmlFileName": ""
}

try {

    if (system.args.length < 11) {
        log("ERROR: Wrong number of parameters for browser.js. Expecting 11 parameters, received: " + system.args.length);
        phantom.exit();
    }

    // Parameters
    var url = system.args[1];
    var pageWidth = parseInt(system.args[2]);
    var pageHeight = parseInt(system.args[3]);
    var scriptName = system.args[4];
    var takeScreenShot = system.args[5];
    var saveRawHtml = system.args[6];
    var instanceId = system.args[7];
    var screenShotFolderPath = system.args[8];
    var rawHtmlFolderPath = system.args[9];
    var parsedDataFilePath = system.args[10];

    // Check for DEBUG flag
    var isDebug = true;
    if (system.args.length > 12) {
        isDebug = (system.args[12].toLowerCase() === "true");
    }

    // Browser settings
    page.viewportSize = {
        width: pageWidth,
        height: pageHeight
    };

    // DEBUG message
    if (isDebug) {
        log("Parameters: ");
        for (var i = 0; i < system.args.length; i++) {
            log("  [" + i + "] " + system.args[i]);
        }
    }

    // Load web page and execute script
    page.open(url, function (status) {
        try {

            // DEBUG message
            if (isDebug) {
                log("Opening page...");
                log("Status: " + status);
            }

            if (status !== 'success') {
                log('status: ' + status);
                parseResult.errors.push(status);
                exitBrowser(parseResult);
            } else {

                // Wait: Give the page a second to complete any internal js call, rendering etc.
                setTimeout(function () {
                    try {

                        // DEBUG message
                        if (isDebug) {
                            log("Creating data variables on page...");
                        }
                        // Create data variables
                        page.evaluate(function () {
                            window.scan_waitTime = 500;
                            window.scan_data = [];
                            window.scan_error = [];
                            window.scan_status = 'pending';
                        });

                        // DEBUG message
                        if (isDebug) {
                            log("Injecting custom script: " + scriptName);
                        }

                        // Inject script to scrap the data
                        if (page.injectJs( 'script/' + scriptName)) {

                            // DEBUG message
                            if (isDebug) {
                                log("Script injection completed.");
                            }

                            // Check if script asked for additional wait time
                            var waitTime = page.evaluate(function () {
                                if (scan_waitTime) {
                                    return scan_waitTime;
                                } else {
                                    // default wait time 100ms;
                                    return 100;
                                }
                            });

                            // DEBUG message
                            if (isDebug) {
                                log("scan_waitTime: " + waitTime);
                            }

                            setTimeout(function () {

                                // DEBUG message
                                if (isDebug) {
                                    log("Calling method to get scan data...");
                                }

                                try {
                                    // Get the data collected by the scrapping script
                                    var scanResult = page.evaluate(function () {
                                        var payload = {};
                                        payload.error = [];
                                        payload.data = scan_data;
                                        payload.status = scan_status;
                                        payload.len = scan_data.length;
                                        payload.isFile = false;
                                        if (scan_error) {
                                            //TODO: Check error type and convert to string
                                            payload.error.push(scan_error);
                                        }
                                        return payload;
                                    });

                                    // DEBUG message
                                    if (isDebug) {
                                        log("Payload collected. " + scanResult.len);
                                    }

                                    parseResult.errors = scanResult.error;
                                    parseResult.status = scanResult.status;
                                    parseResult.count = scanResult.len;
                                    parseResult.data = scanResult.data;
                                    parseResult.instanceId = instanceId;

                                } catch (ex) {
                                    log(ex);
                                }

                                // DEBUG message
                                if (isDebug) {
                                    log("Processing payload...");
                                }


                                parseResult.scripts = parseResult.scripts || [];
                                parseResult.scripts.push(scriptName);

                                try {

                                    // Write the collected data on a file and send the file name

                                    if (isDebug) {
                                        log("Saving collected data on file:" + parsedDataFilePath);
                                    }

                                    var fileContent = JSON.stringify(parseResult);
                                    fs.write(parsedDataFilePath, fileContent, 'w');

                                    if (isDebug) {
                                        log("Data saved on file:" + parsedDataFilePath);
                                    }

                                    // Screen Shot
                                    if (isDebug) {
                                        log("Take screen shot?: ", takeScreenShot);
                                    }
                                    if (takeScreenShot.toLowerCase() === 'true') {
                                        var screenShotId = instanceId;
                                        var imgPath = screenShotFolderPath + "/" + screenShotId + '.png';
                                        page.render(imgPath);

                                        parseResult.screenShotFileName = imgPath;

                                        if (isDebug) {
                                            log("screenshot path: ", imgPath);
                                        }
                                    }

                                    // Save Raw HTML
                                    log("Save raw html?: ", saveRawHtml);
                                    if (saveRawHtml.toLowerCase() === 'true') {
                                        var rawHtmlContentId = instanceId;
                                        var htmlFilePath = rawHtmlFolderPath + "/" + rawHtmlContentId + '.html';
                                        fs.write(htmlFilePath, page.content, 'w');

                                        parseResult.rawHtmlFileName = htmlFilePath;

                                        if (isDebug) {
                                            log("Raw html file path: ", htmlFilePath);
                                        }
                                    }

                                    exitBrowser(parseResult);
                                } catch (err) {
                                    parseResult.errors = parseResult.errors || [];
                                    parseResult.errors.push(err.message);
                                    exitBrowser(parseResult);
                                }

                            }, waitTime);

                        } else {
                            parseResult.errors = parseResult.errors || [];
                            parseResult.errors.push("Failed to load parser script: " + scriptName);
                            exitBrowser(parseResult);
                        }
                    } catch (ex) {
                        parseResult.errors = parseResult.errors || [];
                        parseResult.errors.push(ex.message);
                        exitBrowser(parseResult);
                    }
                }, 1000);
            }

        } catch (pageOpenError) {
            parseResult.errors = parseResult.errors || [];
            parseResult.errors.push(pageOpenError.message);
            exitBrowser(parseResult);
        }
    });
}
catch (outerError) {
    parseResult.errors = parseResult.errors || [];
    parseResult.errors.push(outerError.message);
    exitBrowser(parseResult);
}


function exitBrowser(result) {
    // DEBUG message
    if (isDebug && result.errors) {
        log("Errors:");
        log(JSON.stringify(result.errors));
    }

    log("$$ScriptRunner::ExitBrowser$$");

    phantom.exit();
}

function log(msg) {
    console.log("<br />");
    console.log((new Date()).toLocaleTimeString() + ": " + msg);
}