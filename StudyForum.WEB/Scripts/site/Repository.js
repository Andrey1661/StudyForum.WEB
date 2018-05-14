
function indertFiles(data) {

    if (data) {
        console.log(data);

        var list = $('#side-rep-files');
        list.children().remove();

        $(data).each(function(i, item) {

            var li = $('<li draggable="true">');
            var name = $('<span>');
            var id = $('<span name="fileId" hidden="hidden">');

            name.html(item.Name);
            id.html(item.Id);
            li.append(name);
            li.append(id);
            list.append(li);
        });
    }

}