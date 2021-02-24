$(document).ready(function () {


    $('#UserDetails').hide();
    $('#project_List').hide();

    var userRole = document.getElementById("UserDetails").value;

    if (userRole == "IC Admin") {
        $('#project_List').show();


    }



    //   $('#Response').hide();
    $('#divAddNewQuestion').hide();
    $('#questionGroup').hide();
    $("#continueChek").hide();

    $('.multiselect').multiselect();

});

function checkList_Save() {
    var checkListName = document.getElementById("CheckListName_VC").value;
    var ProjectName = document.getElementById("Project_Name").value;
    var details = {
        CheckListName_VC: checkListName,
        Project_Name: ProjectName

    }
    $.ajax(
        {
            type: "POST",
            data: JSON.stringify(details),
            url: "@Url.Action('CheckListConfiguration', 'CheckListConfiguration')",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {

                if (data != 1) {
                    $('#questionGroup').show();
                    $("#model_value").text("CheckList has been Saved Succesfully.....");
                    $('#myModal').modal('show');
                    $("#CheckListName_VC").prop('disabled', true);
                    $("#checkListSave").prop('disabled', true);

                }
                else {
                    $("#continueChek").show();
                    $("#model_value").text("Already Registered.....");

                    $("#modal_button").click();
                    //$('#myModal').modal('show');
                }




            }
        })

}

function save_questionGroup() {


    $.post("@Url.Action('CheckListConfiguration', 'CheckListConfiguration')", $('#formid').serialize(),
        function (data) {

            if (data == 1) {
                $("#continueChek").hide();
                $("#lblSuccessmsg").text("Records Updated Succesfully.....");

                //$('#myModal').modal('show');
                $("#btnSuccessAlert").click();
                $('input[name="questionGroup"]').prop('disabled', true);
                $('input[name="Add_Row"]').prop('disabled', true);
                $('input[name="allowableResponse"]').prop('disabled', true);
                // $('select[name="responseType"]').prop('disabled', true);



                $('input[name="validity"]').prop('disabled', true);
                $("input[name*='IsDefault']").prop('disabled', true);
                $('input[name="savequestionGroup"]').prop('disabled', true);

                $('input[name="Add_Question"]').prop('disabled', true);
                $('input[name="questionName"]').prop('disabled', true);
                $('input[name="delete_Question"]').prop('disabled', true);
                $('input[name="Trade_ID"]').prop('disabled', true);





            }


        });



}
function Add_Question_Row(oButton) {


    var row = document.getElementById("questionResponse_1"); // find row to copy
    var table = document.getElementById("questionGroupTable"); // find table to append to
    var clone = row.cloneNode(true);
    // clone.lastElementChild.innerHTML = '<button id="removeRow" class="btn btn-block btn-success" onclick="removeRecords(this)">delete</button>'

    var InputType = clone.getElementsByTagName("input");
    for (var i = 0; i < InputType.length; i++) {
        if (i == 0 && InputType[i].id == "questionResponse_1") {
            InputType[i].id = "questionResponse_" + document.getElementById("questionGroupTable").rows.length.toString();
        }
        if (InputType[i].id != "Add_Row") {
            InputType[i].value = '';
        }

        // document.getElementById("Add_Row_"+[i]).value = "PLUS";

    }




    var SelectType = clone.getElementsByTagName("select");
    //for (var i = 0; i < SelectType.length; i++) {

    //    SelectType[i].value = '--Select--';

    //}



    var buttonType = clone.getElementsByTagName("button");
    for (var i = 0; i < buttonType.length; i++) {
        if (i == 0) {
            buttonType[i].id = "Add_Row_" + document.getElementById("questionGroupTable").rows.length.toString();
            // document.getElementById("Add_Row_" + [i]).value = "Add";


        }


    }

    clone.id = "questionResponse_" + document.getElementById("questionGroupTable").rows.length.toString();

    //document.getElementsByClassName('table_Row')[0].setAttribute('value', 'navbar-Add');
    // document.getElementsByClassName("table_Row").setAttribute('value', 'Add');

    table.appendChild(clone);






}





function removeRecords(oButton) {


    var result = confirm("Do you want to delete the record?");
    if (result == true) {
        var empTab = document.getElementById('questionGroupTable');
        empTab.deleteRow(oButton.parentNode.parentNode.rowIndex);

    }
    else {
        return false;
    }


}
var x;
function AddQuestions(oButton) {

    $('#divAddNewQuestion').show();

    var btnId = oButton.id;
    var y = btnId;
    var a = y.substr(13);


    $("#questionGroupTable").show();
    var z = $("#questionGroupName_").val();
    if (z != null && $("#questionGroupName_" + a).val() == null) {
        x = document.getElementById("questionGroupName_").value;
    }
    else {
        x = document.getElementById("questionGroupName_" + a).value;
    }

    var markup = "<tr><td><input type='Text' class='form-control' id='questionName' Name='questionName' placeholder='Enter Question'></td><td><input type='Text' class='form-control' id='allowableResponse' Name='allowableResponse' placeholder='Enter Allowable Response eg: x,y'></td><td><select id='responseType'class='form-control' name='responseType'><option>Text</option><option>Dropdown</option><option>Checkbox</option><option>RadioButton</option><option>Number</option> </select></td></td><td><input type='submit' value='delete' name='delete_Question'  onclick='return removeRecords(this)'class='btn btn-success'></td></tr>";

    $("#questionData").append(markup);
    //  $('#Response').show();

    // Find and remove selected table rows
    $("#delete").click(function () {

        var rseult = confirm("Do you want to delete the record?");
        if (r == true) {
            $("table tbody").find('input[name="record"]').each(function () {
                if ($(this).is(":checked")) {
                    $(this).parents("tr").remove();
                }
            });
        }

    });

}
