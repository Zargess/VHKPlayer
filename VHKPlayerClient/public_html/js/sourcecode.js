function openConnection() {
    // uses global 'conn' object
    if (conn.readyState === undefined || conn.readyState > 1) {
        conn = new WebSocket('ws://localhost:8100');

        conn.onopen = function() {
            conn.send("Connection Established Confirmation");
        };

        conn.onmessage = function(event) {
            document.getElementById("content").innerHTML = event.data;
        };

        conn.onerror = function(event) {
            alert("Web Socket Error");
        };

        conn.onclose = function(event) {
            alert("Web Socket Closed");
        };
    }
}

$(document).ready(function() {
    conn = {}, window.WebSocket = window.WebSocket || window.MozWebSocket;
    $("#btn").click(function() {
        conn.send("Hello World");
    });
    openConnection();
    $('.tabs .tab-links a').on('click', function(e)  {
        var currentAttrValue = $(this).attr('href');
 
        // Show/Hide Tabs
        $('.tabs ' + currentAttrValue).show().siblings().hide();
 
        // Change/remove current tab to active
        $(this).parent('li').addClass('active').siblings().removeClass('active');
 
        e.preventDefault();
    });
});