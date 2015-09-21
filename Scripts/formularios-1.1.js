var msgs_pendientes = [];
var formularios_titulo;
var formularios_enfocarControles = null;
var formularios_imgCargando = "images/cargando.gif";
var formularios_dialogReference="#pagina";

function formularios_submit(event) {
    if (typeof formularios_omitirsubmitdialog!=="undefined"&&formularios_omitirsubmitdialog) return;
    
    openShadowBoxHTML(250, 100, formularios_titulo, "<div><img style=\"float:left;margin-right:10px;\" src=\"" + formularios_imgCargando + "\" alt=\"Espere por favor...\" /> <div style=\"text-align:center;font-size:small\">Espere un momento por favor...</div></div>",0,$(document).scrollTop());
}

function formularios_ScrollView(ctrls) {
    var yOffset = 0;
    for (var i = 0; i < ctrls.length; i++) {
        var el = document.getElementById(ctrls[i]);
        if (el != null) {
            if (i == 0) {
                el.scrollIntoView();
                yOffset = $(el).offset().top();
            }
            el.style.backgroundColor = '#FFFFDD';
            el.focus();
        }
    }
    return yOffset;
}

$(function () {
    var xOffset = parseInt($("#__SCROLLPOSITIONX").val());
    var yOffset = parseInt($("#__SCROLLPOSITIONY").val());

    if (formularios_enfocarControles != null) {
        yOffset = formularios_ScrollView(formularios_enfocarControles);
    }

    var panelesModales = $(".panel-dialog");
    for (var i = 0; i < panelesModales.length; i++) {
        var dom = panelesModales.eq(i);
        var width = 0;
        var hijos=dom.children();
        for (var j = 0; j < hijos.length; j++) {
            var h = hijos.eq(j);
            var width_j = h.width();
            if (width < width_j) width = width_j;
        }
        openShadowBoxPanel(dom, width + 60, formularios_titulo, xOffset, yOffset)
    }

    var mensajesInfo = $(".mensaje-info-dialog");
    for (var i = 0; i < mensajesInfo.length; i++) {
        var m = mensajesInfo.eq(i).text();
        mensajeInfo(formularios_titulo, m, xOffset, yOffset);
    }

    var mensajesWarning = $(".mensaje-warning-dialog");
    for (var i = 0; i < mensajesWarning.length; i++) {
        var m = mensajesWarning.eq(i).text();
        mensajeWarning(formularios_titulo, m, xOffset, yOffset);
    }

    var mensajesError = $(".mensaje-error-dialog");
    for (var i = 0; i < mensajesError.length; i++) {
        var m = mensajesError.eq(i).text();
        mensajeError(formularios_titulo, m, xOffset, yOffset);
    }

    $("form").submit(formularios_submit);
});

function mensajeBox(titulo, mensaje, width, icono, xOffset, yOffset) {
    var dlg = $("<div><div class=\"mensaje-" + icono + "\">" + mensaje + "</div></div>").appendTo(formularios_dialogReference);

    dlg.dialog({
        closeOnEscape: true,
        modal: true,
        title: titulo,
        autoOpen: true,
        width: width,
        resizable: false,
        autoResize: true,
        buttons: [
          {
              text: "Aceptar",
              click: function () {
                  $(this).dialog("close");
              }
          }
        ],
        open: function (event, ui) {
            var w = $(window);
            var d = $(this).parent();
            var x = (w.width() - d.width()) / 2 + xOffset;
            var y = (w.height() - d.height()) / 2 + yOffset;
            d.css("position", "absolute");
            d.css("top", y);
            d.css("left", x);
        },
        close: function (event, ui) {
            $(this).remove();
        }
    });

    return dlg;
}

function mensajeInfo(titulo, mensaje, xOffset, yOffset) {
    return mensajeBox(titulo, mensaje, 300, "info", xOffset, yOffset);
}
function mensajeError(titulo, mensaje, xOffset, yOffset) {
    return mensajeBox(titulo, mensaje, 300, "error", xOffset, yOffset);
}
function mensajeWarning(titulo, mensaje, xOffset, yOffset) {
    return mensajeBox(titulo, mensaje, 300, "warning", xOffset, yOffset);
}

function registrarConfirmButton(btn, titulo, mensaje, txtAceptar, txtCancelar, width, cookie) {
    for (var i = 0; i < btn.length; i++) {
        var btn_i = btn.eq(i);
        var oldOnClick = btn_i.attr("onclick");
        btn_i.attr("onclick","");
        var ev = {
            btn: btn_i[0],
            titulo: titulo,
            mensaje: mensaje,
            txtAceptar: txtAceptar,
            txtCancelar: txtCancelar,
            width: width,
            cookie: cookie,
            oldOnClick: oldOnClick,
            clic: function () {
                return confirmButton(this.btn, this.titulo, this.mensaje, this.txtAceptar, this.txtCancelar, width, cookie, this.oldOnClick);
            }
        };
        btn_i.click($.proxy(ev.clic, ev));
    }
}

function confirmButton(btn, titulo, mensaje, txtAceptar, txtCancelar, width, cookie, oldOnClick) {
    var nopreguntar=false;
    if (cookie !== undefined && cookie !== false) {
        nopreguntar = $.cookie(cookie);
    }
    if (btn.enviar == true || nopreguntar) {
        if (oldOnClick !== undefined) {
            var r = eval(oldOnClick);
            if (r) $(this).dialog("close");
            return r;
        }
        else {
            return true;
        }
    }
    else {
        var html = '<div><div class="mensaje-pregunta">' + mensaje + '</div>';

        if (cookie !== undefined && cookie !== false) {
            html += '<div class="mensaje-nopreguntar"><input type="checkbox" id="chkNoPreguntar" checked="checked" /><label for="chkNoPreguntar">No volver a preguntar</label></div>';
        }
        html += '</div>';

        var dlg = $(html).appendTo(formularios_dialogReference);
        dlg.dialog({
            btn: btn,
            closeOnEscape: false,
            open: function (event, ui) {
                var d = $(this).parent();
                d.find(".ui-dialog-titlebar-close").css("display", "none");
            },
            modal: true,
            title: titulo,
            zIndex: 10000,
            autoOpen: true,
            width: width,
            resizable: false,
            autoResize: true,
            buttons: [
              {
                  text: txtAceptar,
                  click: function () {
                      var btn = $(this).dialog("option", "btn");
                      if (cookie !== undefined && cookie !== false) {
                          var chk = document.getElementById("chkNoPreguntar");
                          if (chk.checked) {
                              $.cookie(cookie, true, { expires: 999 });
                          }
                      }
                      $(this).dialog("close");

                      if (oldOnClick !== undefined) {
                          var r = eval(oldOnClick);
                          if (r) $(this).dialog("close");
                          return r;
                      }
                      else {
                          btn.enviar = true;
                          btn.click();
                      }
                  }
              },
              {
                  text: txtCancelar,
                  click: function () {
                      $(this).dialog("close");
                  }
              }
            ],
            close: function (event, ui) {
                $(this).remove();
            }
        });
        return false;
    }
}

function openShadowBox(url, width, height, title, onclose) {
    var marco;
    width = parseInt(width)+60;
    var horizontalPadding = 30;
    var verticalPadding = 30;
    marco = $('<iframe id="ventanaModal" src="' + url + '" frameBorder="0"/>').appendTo(formularios_dialogReference);
    marco.dialog({
        title: title,
        autoOpen: true,
        minWidth: 400,
        minHeight: 300,
        width: width,
        height: height,
        modal: true,
        resizable: true,
        autoResize: true,
        closeOnEscape: true,
        close: onclose
    });
    marco.width(width - horizontalPadding).height(height - verticalPadding);

    return marco;
}


function openShadowBoxPanel(dom, width, title, xOffset, yOffset) {
    dom.dialog({
        title: title,
        autoOpen: true,
        minWidth: 400,
        minHeight: 300,
        width:width,
        modal: true,
        resizable: true,
        autoResize: true,
        closeOnEscape: false,
        open: function (event, ui) {
            $(this).parent().appendTo(formularios_dialogReference);
            var w = $(window);
            var d = $(this).parent();
            var x = (w.width() - d.width()) / 2 + xOffset;
            var y = (w.height() - d.height()) / 2 + yOffset;
            d.css("position", "absolute");
            d.css("top", y);
            d.css("left", x);
            d.find(".ui-dialog-titlebar-close").css("display", "none");
        },
        close: function (event, ui) {
            $(this).remove();
        }
    });

    return dom;
}

function openShadowBoxHTML(width, height, title, html, xOffset, yOffset) {
    var dlg = $(html).appendTo(formularios_dialogReference);
    dlg.dialog({
        title: title,
        minWidth: 200,
        minHeight: 100,
        width: width,
        height: height,
        modal: true,
        autoOpen: true,
        resizable: false,
        autoResize: false,
        closeOnEscape: false,
        position: null,
        open: function (event, ui) {
            $(this).parent().appendTo(formularios_dialogReference);
            var w = $(window);
            var d = $(this).parent();
            var x = (w.width() - d.width()) / 2 + xOffset;
            var y = (w.height() - d.height()) / 2 + yOffset;
            d.css("position", "absolute");
            d.css("top", y);
            d.css("left", x);
        },
        close: function (event, ui) {
            $(this).remove();
        }
    });

    return dlg;
}
