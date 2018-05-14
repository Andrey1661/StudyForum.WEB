
$(document).ready(function() {



    $('#inputGroup').on('input', function() {

        var input = $(this);
        var value = input.val();

        if (value.length > 0) {

            var list = $('#groupList');
            var options = list.children();
            for (var i = 0; i < options.length; i++) {
                if (value === $(options[i]).attr('value')) {
                    $("#GroupId").val($(options[i]).attr('data'));
                    options.remove();
                    return;
                }
            }

            $.ajax({
                url: '/api/Groups',
                data: {
                    searchPattern: input.val(),
                    maxCount: 10
                },
                dataType: 'json',
                success: function (data) {
                    
                    list.children().remove();

                    $(data).each(function(i, item) {
                        var elem = $('<option>');
                        elem.attr('value', item.Name);
                        elem.attr('data', item.Id);
                        list.append(elem);
                    });
                }
            });
        }

    });

});