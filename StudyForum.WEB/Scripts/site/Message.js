$(document).ready(function () {

    var cont = $("#filesContainer");
    var input = createFileInput(0);
    cont.append(input);
});

function onInputChange(event) {
    var cont = $("#filesContainer");
    var input = $(event.target);

    if (processFileInput(input)) {
        var inputCount = cont.children("label.inputFileWrapper").length;
        var div = createFileInput(inputCount);     
        cont.append(div);
    }  
}

function createFileInput(number) {
    var label = $("<label class='inputFileWrapper' id='file_" + number + "'>");
    var input = $("<input type='file'>");

    input.change(onInputChange);
    label.append("Загрузить файл");
    label.append(input);

    return label;
}

function processFileInput(input) {
    if (!input || !input.prop('files'))
        return false;

    var file = input.prop('files')[0];
    if (file.size > 0) {
        var inputNumber = input.parent().attr('id').split('_')[1];
        createFileTag(inputNumber, file.name, file.size);
        input.attr('name', 'attachedFiles');
        input.parent().hide();
        return true;
    }

    return false;
}

function createFileTag(number, name, size) {
    var cont = $("<div class='fileTagContainer' id='tag_" + number + "'>");
    var nameCont = $("<div>");
    var sizeCont = $("<div>");
    var lbName = $("<span class='fileTagName'>");
    var lbSize = $("<span class='fileTagSize'>");
    lbName.html(name);
    lbSize.html(size + 'KB');
    nameCont.append(lbName);
    sizeCont.append(lbSize);
    cont.append(nameCont);
    cont.append(sizeCont);
    cont.click(onFileTagClick).children(function(e) { e.stopPropagation() });
    $("#fileTagsContainer").append(cont);
}

function onFileTagClick() {
    var number = $(this).attr('id').split('_')[1];
    var cont = $("#filesContainer");
    var inputWrap = cont.children("#file_" + number);
    inputWrap.remove();
    $(this).remove();

    var tagCont = $("#fileTagsContainer");

    for (var i = 0; i < cont.children().length; i++) {
        inputWrap = $(cont.children()[i]);
        var num = inputWrap.attr('id').split('_')[1];
        if (num > number) {
            inputWrap.attr('id', 'file_' + (num - 1));
            var input = $(inputWrap.children('input')[0]);

            if (input.prop('files') && input.prop('files')[0]) {
                var tag = $(tagCont.children('#tag_' + num)[0]);
                tag.attr('id', 'tag_' + (num - 1));
            }
        }
    }
}