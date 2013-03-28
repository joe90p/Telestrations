$(document).ready(start);

function start() {
    
    var submit = document.getElementById("submit");
    var write = document.getElementById("write");
    var name = document.getElementById("name");
    var ws = new WebSocket('ws://localhost:2011/');
    submit.addEventListener("click", connect);
    
    var socketOpen = false;
    
    ws.onopen = function () {
        socketOpen = true;
    };
    
    ws.onmessage = function (m) {
        write.innerHTML += "<div>" + m.data + "</div>";
    };
    
    function connect() {
        
        if (socketOpen) {
            ws.send(name.value);
        } else {
            alert('socket not open');
        }
    }

    

    

}