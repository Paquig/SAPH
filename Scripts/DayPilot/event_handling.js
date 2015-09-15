//function create(start, end, resource) {
//    createModal().showUrl('Nuevo.aspx?tstart=' + start + "&tend=" + end + "&resource=" + resource);
//}
function create(start, end, resource, id_horario) {
    createModal().showUrl('Nuevo.aspx?idhorario=' + id_horario + "&tstart=" + start + "&tend=" + end + "&resource=" + resource);
}
function edit(e) {
    createModal().showUrl('Editar.aspx?idRecurso=' + e.value());
   }

function editBlock(e) {
    var start = e.start.substring(0, e.start.indexOf(':'));
    createModal().showUrl("EditBlock.aspx?id=" + start);
}

function createModal() {
    var modal = new DayPilot.Modal();
    modal.top = 60;
    modal.width = 460;
    modal.opacity = 50;
    modal.border = "10px solid #d0d0d0";
    modal.closed = function () {
     //   if (this.result == "Yes") {
            dp.commandCallBack("refresh");
            //, { message: this.result.message });
            //}
                dp.clearSelection();
    };

    modal.setHeight = function (height) {
        modal.height = height;
        return modal;
    };

    modal.height = 300;
    modal.zIndex = 100;

    return modal;
}
