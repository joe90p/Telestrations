$(document).ready(start);

function start() {
    Draw();

};

function getOffset(el) {
    var x = y = 0;
    if (el.offsetParent) {
        do {
            x += el.offsetLeft;
            y += el.offsetTop;
        } while(el = el.offsetParent)
    }
    return { "x": x, "y": y };
}

function Draw() {
    var xhr;
    var drawCanvas = document.getElementById("draw");
    var submitDraw = document.getElementById("sendDrawing");
    var drawHub = $.connection.chatHub;
    var register = document.getElementById("register");
    var testLoader = document.getElementById("testLoader");
    var playArea = document.getElementById("playArea");

    register.addEventListener('click', registerMe);
    
    submitDraw.addEventListener('click', submitDrawClick);
    var draw = false;
    var canvasCtx = drawCanvas.getContext("2d");
    var os = getOffset(drawCanvas);


    testLoader.addEventListener("click", testLoad);
    
    function testLoad() {
        xhr = new XMLHttpRequest();
        xhr.open("GET", "WrittenGuess.html", true);
        xhr.onreadystatechange = onchange;
        xhr.send();

    }
    
    function onchange(evt) {
        var req = evt.currentTarget; 

        switch (req.readyState) {
            case XMLHttpRequest.DONE:
            case 4:
                var hasResponse = req.status === 200 || req.status === 0;
                playArea.innerHTML = hasResponse ? req.responseText : "<h1>Unable to load text file: status=" + req.status + "</h1>";
                break;
        }
    }
    
    function registerMe() {
        drawHub.server.register();
    }
    
    function submitDrawClick() {

        var im = drawCanvas.toDataURL("image/png");
        im = im.replace('data:image/png;base64,', '');
        
        $.ajax({
            type: "POST",
            url: "Service2.svc/UploadImage",
            data: '{ "image" : "' + im + '", "id" : "' + drawHub.connection.id + '" }',
            contentType: 'application/json; charset=utf-8',
            success: success,
            error: fail
        });

    }
    
    drawHub.client.broadcastMessage = function (name, message) {
        // Html encode display name and message. 
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page. 
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };


    $.connection.hub.start().done(function () {
        submitDraw.addEventListener('click', submitDrawClick);
    });

    drawCanvas.addEventListener('mousedown', function () {
        draw = true;
    });
    
    drawCanvas.addEventListener('mouseup', function () {
        draw = false;
    });
    
    drawCanvas.addEventListener('mouseout', function () {
        draw = false;
    });
    
    drawCanvas.addEventListener('mousemove', pencilDraw);
    
    function pencilDraw(e) {
        if (draw)
            {
        
        var point = { x: e.clientX - os.x, y: e.clientY - os.y };
        canvasCtx.fillStyle = "#000000";
        canvasCtx.fillRect(point.x, point.y, 3, 3);
        }


    }
};


function DrawnGuess() {
    var drawCanvas = document.getElementById("draw");
    var submitDraw = document.getElementById("sendDrawing");
}