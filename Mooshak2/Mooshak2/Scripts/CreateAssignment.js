$(document).ready(function () {

    var subassignment = $(".subassignment");
    var addsubassignmentbutton = $(".addsubassignment");
    var subassnumber = 2;

    addsubassignmentbutton.on("click", function () {
        subassignment.append($("<div class='subassignment'>"
            + "    <h3>Sub-Assignment " + subassnumber + "</h3>"
            + "    <div class='form-group'>"
            + "        <label>"
            + "            Sub-Assignment " + subassnumber + " title"
            + "            <input type='text' class='form-control' id='MilestoneTitle" + subassnumber + "' name='MilestoneTitle" + subassnumber + "'>"
            + "        </label>"
            + "    </div>"
            + "    <div class='form-group'>"
            + "        <label>"
            + "            Submission limit"
            + "            <select name='SubmissionLimit" + subassnumber + "' class='form-control' id='SubmissionLimit" + subassnumber + "'>"
            + "                <option value='1'>1</option>"
            + "                <option value='2'>2</option>"
            + "                <option value='3'>3</option>"
            + "                <option value='4'>4</option>"
            + "                <option value='5'>5</option>"
            + "                <option value='6'>6</option>"
            + "                <option value='7'>7</option>"
            + "                <option value='8'>8</option>"
            + "                <option value='9'>9</option>"
            + "                <option value='10'>10</option>"
            + "            </select>"
            + "        </label>"
            + "    </div>"
            + "    <div class='form-group input-pairs'>"
            + "        <label>"
            + "            Input 1"
            + "            <input type='text' class='form-control' id='MileStone" + subassnumber + "_Input1' name='MileStone" + subassnumber + "_Input1'>"
            + "        </label>"
            + "        <label>"
            + "            Output 1"
            + "            <input type='text' class='form-control' id='MileStone" + subassnumber + "_Output1' name='MileStone" + subassnumber + "_Output1'>"
            + "        </label>"
            + "    </div>"
            + "    <input type='button' value='+ Add input/output' class='btn btn-info addinpoutp'>"
            + "</div>"
            + "<br />"));
        subassnumber++;
    });

    var inputpairs = $(".input-pairs");
    var addpairbutton = $(".addinpoutp");
    var pairnumber = 2;

    $(".addinpoutp").on("click", function () {
        $(this).prev().append($("<div class='form-group input-pairs'>"
            + "    <label>"
            + "        Input " + pairnumber
            + "        <input type='text' class='form-control' id='MileStone" + subassnumber + "_Input" + pairnumber + "' name='MileStone" + subassnumber + "_Input" + pairnumber + "'>"
            + "    </label>"
            + "    <label>"
            + "        Output " + pairnumber
            + "        <input type='text' class='form-control' id='MileStone" + subassnumber + "_Output" + pairnumber + "' name='MileStone" + subassnumber + "_Output" + pairnumber + "'>"
            + "    </label>"
            + "</div>"));
        pairnumber++;
    });
});