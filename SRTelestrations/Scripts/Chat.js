﻿$(document).ready(start);

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
    var drawHub;
    var register;
    var testLoader;
    var playArea;
    var getGuess;

    Initialise();
    
    function Initialise() {

        isDrawnGuess = true;
        drawHub = $.connection.gameHub;

        register = document.getElementById("register");
        testLoader = document.getElementById("testLoader");
        playArea = document.getElementById("playArea");
        getGuess = document.getElementById("getGuess");       
        
        $.connection.hub.start().done(function () {
            register.addEventListener('click', registerMe);
            getGuess.addEventListener('click', requestGuess);
        });

        drawHub.client.broadcastMessage = broadcast;
        drawHub.client.roundChange = roundChange;
    }
    
    function roundChange(toGuess, isDrawnGuess, isNewGame) {
        xhr = new XMLHttpRequest();
        var url = isDrawnGuess ? "DrawnGuess.html" : "WrittenGuess.html";
        xhr.open("GET", url, true);
        var writtenFn = function (g) { return WrittenGuess(g, isNewGame) };
        xhr.onreadystatechange = isDrawnGuess ? onResponseChange(DrawnGuess, toGuess) : onResponseChange(writtenFn, toGuess);
        xhr.send();
    }
    
    function broadcast(name, message) {
        // Html encode display name and message. 
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page. 
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    }     
    
    function onResponseChange(fn, toGuess) {
        return function (evt) {
            var req = evt.currentTarget;

            switch (req.readyState) {
            case XMLHttpRequest.DONE:
            case 4:
                var hasResponse = req.status === 200 || req.status === 0;
                $(playArea).show();
                playArea.innerHTML = hasResponse ? req.responseText : "<h1>Unable to load text file: status=" + req.status + "</h1>";
                if (hasResponse) {
                    fn(toGuess);
                }

                break;
            }
        };
    }
    
    function registerMe() {
        console.log("registering");
        drawHub.server.register();
    }

    function requestGuess() {
        var x = { id: 'blah' };
        
        $.ajax({
            type: "GET",
            url: "PictureLinkGameService.svc/GetPlaySession?id=" + drawHub.connection.id,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                var data = response.d;
                roundChange(data.PreviousGuess, data.GuessType === 'D', data.PlayType === 'N');
            },
            error: function (txt) {
                alert('error ' + txt.status + ' ' + txt.statusText);
            },
            cache: false
        });
    }
    
    function DrawnGuess(toGuess) {
        var drawCanvas = document.getElementById("draw");
        var submitDraw = document.getElementById("sendDrawing");
        imageGuess = document.getElementById("imageGuess");

        submitDraw.addEventListener('click', submitDrawClick);

        var draw = false;
        var canvasCtx = drawCanvas.getContext("2d");
        var os = getOffset(drawCanvas);
        SetDrawCanvas();
        imageGuess.innerHTML = toGuess;


        function submitDrawClick() {

            var im = drawCanvas.toDataURL("image/png");
            im = im.replace('data:image/png;base64,', '');

            $.ajax({
                type: "POST",
                url: "PictureLinkGameService.svc/UploadImage",
                data: '{ "image" : "' + im + '", "id" : "' + drawHub.connection.id + '" }',
                contentType: 'application/json; charset=utf-8'
            });

            $(playArea).hide();

        }

        function SetDrawCanvas() {
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
        }

        function pencilDraw(e) {
            if (draw) {

                var point = { x: e.clientX - os.x, y: e.clientY - os.y };
                canvasCtx.fillStyle = "#000000";
                canvasCtx.fillRect(point.x, point.y, 3, 3);
            }
        }

    }
    
    function WrittenGuess(toGuess, isNewGame) {
        var label = document.getElementById("testSpan");
        var textGuess = document.getElementById("sendTextGuess");
        var writtenGuess = document.getElementById("writtenGuess");
        var imageHolder = document.getElementById("imageContainer");
        if (toGuess !== null && isNewGame !== true)
            {
        imageHolder.src = "data:image/png;base64," + toGuess;
        writtenGuess.value = toGuess;
        label.innerHTML = toGuess;
        }

        if (isNewGame) {
            label.innerHTML = "New Game";
        }


        textGuess.addEventListener('click', submitTextGuess);
        
        function submitTextGuess() {
            var guess = writtenGuess.value;
            drawHub.server.addWriitenGuess(guess);
            $(playArea).hide();
        }

    }
 
};


