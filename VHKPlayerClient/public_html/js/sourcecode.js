var folders = [];

$(document).ready(function() {
    conn = {}, window.WebSocket = window.WebSocket || window.MozWebSocket;
    openConnection();

    newTab('Hello, World');
    newTab('Hello, World');

    var item = $('.tab');
    var itemId = $(item).attr('id').split('_')[1];
    $(item).parent().attr('data-current', itemId);
    $(item).first().addClass('tabActiveHeader');

    var pages = $(".tabpage");
    for (var i = 1; i < pages.length; i++) {
        pages[i].style.display = "none";
    }

    $(".tab").click(function() {
        displayPage(this);
        doSend("hello world");
    });
    xmlParserTests();
});

function displayPage(tab) {
    var current = $(tab).parent().attr('data-current'); 
    $('#tabHeader_' + current).removeClass('tabActiveHeader');
    $('#tabpage_' + current).css('display', 'none');
    var ident = $(tab).attr('id').split('_')[1];
    $(tab).addClass('tabActiveHeader');
    $('#tabpage_' + ident).css('display', 'block');
    $(tab).parent().attr('data-current', ident);
}

function openConnection() {
    conn = new WebSocket('ws://localhost:8100');
    
    conn.onopen = function() {
        doSend("Connection Established Confirmation");
        doSend("getStructure");
    };

    conn.onmessage = function(event) {
        var msg = event.data;
        if (msg === "disconnect") {
            location.reload();
        }
        var msgArray = msg.split(' ');
        if (msgArray.length > 0) {
            var res = "";
            for (var i = 1; i < msgArray.length; i++) {
                res = res + " " + msgArray[i];
            }
            
            switch(msgArray[0]) {
                case "xml":
                    folders = parseXml(res);
                    break;
                case "msg":
                    alert(res);
                    break;
            }
        }
    };

    conn.onerror = function(event) {
        alert("Web Socket Error");
    };

    conn.onclose = function(event) {
        alert("Web Socket Closed");
    };
}

function doSend(msg) {
    conn.send(msg);
}

function parseXml(sdoc) {
    var folders = [];
    var xml = $(sdoc);
    $(xml).find('Folder').each(function(index, node) {
        if ($(node).attr('marked') === "True") {
            var path = $(node).attr('path');
            var list = path.split('\\');
            var name = list[list.length - 1];
            var folder = {
                Path: path,
                Name: name
            };
            folders.push(folder);
        }
    });
    return folders;
}

function newTab(name) {
    var count = $('ul li').length;
    var pos = count + 1;
    var newHeader = $('#tabHeader_1').clone().prop({id: "tabHeader_" + pos, class: "tab"});
    newHeader.text(name);
    newHeader.appendTo('#tabsContainer');
    var newContent = $("#tabpage_1").clone().prop({id: "tabpage_" + pos, class: "tabpage"});
    newContent.html('<h2>' + name + '</h2>');
    $('.tabscontent').append(newContent);
}