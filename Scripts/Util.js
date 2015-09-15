function AbrirVentana_Saph(url) {
    var features;
    features = ''
    features = 'width=980, height=600, scrollbars=yes';

    var cmd;
    cmd = url;

    var name;
    name = '_blank';

    AbrirVentana(cmd, name, features);
}

///////////////
//
// Genéricos
//
///////////////

function AbrirVentana(url, name, features) {
    var popWin;
    popWin = window.open(url, name, features, false);
}