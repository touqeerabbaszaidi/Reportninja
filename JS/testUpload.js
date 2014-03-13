 $( "#page" ).live( "pageinit", function(){

     $("form").submit(function () {
         alert("yes");
                $.mobile.showPageLoadingMsg();
                
                $.ajax({
                          url: "http://www.ardentautomation.com/voteguard/voteguard_login.php",
                          type: "POST",
                        data: $("form#signform").serialize(),
                          success: function( response ) {
                                  alert('User Request OK');
                                $.mobile.changePage( "home.html", { data: {"logdata": response}} );
                          },
                        error: function( jqXHR, textStatus, errorThrown ) {
                                $.mobile.hidePageLoadingMsg();
                                //console.log('Status: ' + textStatus + "\nError: " + errorThrown);
                                alert('User Request Failed');
                        }
                });
                
                return false; // Prevent a form submit
        });        
});

$( "#homepage" ).live( "pagebeforecreate", function(){
        var logdata = getParameterByName( "logdata", $( this ).jqmData( "url" ));
        $( ".logdata" ).append(logdata);
});

function getParameterByName(param, url) { 
        var match = RegExp('[?&]' + param + '=([^&]*)').exec(url);  
        return match && decodeURIComponent(match[1].replace(/\+/g, ' '));  
}

function captureVideo() {
    // Launch device video recording application, 
    // allowing user to capture up to 2 video clips
    navigator.device.capture.captureVideo(captureSuccess, captureError, {limit: 1, duration: 10});
}

//Called when capture operation is finished
//
function captureSuccess(mediaFiles) {
    var i, len;
    len = mediaFiles.length;
    //alert(len);
    for (i = 0, len; i < len; i += 1) {
        uploadFile(mediaFiles[i]);
    }       
}

// Called if something bad happens.
// 
function captureError(error) {
    var msg = 'An error occurred during capture: ' + error.code;
    navigator.notification.alert(msg, null, 'Uh oh!');
}
//Upload files to server
function uploadFile(mediaFile) {
    var ft = new FileTransfer(),
        path = mediaFile.fullPath,
        name = mediaFile.name;
    var options = new FileUploadOptions();
    options.chunkedMode = true;
    options.fileKey = "file";
    options.fileName = name;
    options.mimeType = "video/mpeg";
    var params = new Object();
    params.value1 = "test";
    params.value2 = "param";

    options.params = params;

    ft.upload(path, "http://projectxmobile.2bvision.com/upload.php",
        function (result) {
            console.log('Upload success: ' + result.responseCode);
            console.log(result.bytesSent + ' bytes sent');
            console.log("Response = " + r.response);
            alert("Response = " + r.response);
        },
        function (error) {
            console.log('Error uploading file ' + path + ': ' + error.code);
            alert('Error uploading file ' + path + ': ' + error.code);
        },
        options);
    alert(mediaFile.fullPath);
}

function win(r) {
    console.log("Code = " + r.responseCode);
    console.log("Response = " + r.response);
    console.log("Sent = " + r.bytesSent);
    alert(r.response);
}

function fail(error) {
    alert("An error has occurred: Code = " = error.code);
}