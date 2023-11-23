$(document).ready(function () {

    $("#btnCreate").click(function () {

        let receita = {};

        receita.titulo        = $('.txtTituloReceita').val();
        receita.descricao     = $('.txtDescrReceita').val();
        receita.modoDePreparo = $('.txtModoPrepReceita').val();
        receita.foto          = $('.txtFotoReceita').val();
        receita.categoria     = $('.txtCategoriaReceita').val();
        receita.ingredientes  = [];
        receita.tags          = [];

        var nomesIngredientes = $('.txtNomeIngrediente').val()
        var arrayDeNomes = nomesIngredientes.split(',');

        var qtdesIngredientes = $('.txtQtdeIngrediente').val();
        var arryQtdeIngredientes = qtdesIngredientes.split(',');

        var unidadeIngredients = $('.txtUnidadeIngrediente').val();
        var arrayUnidadeIngredientes = unidadeIngredients.split(',');

        if (arrayDeNomes.length > arryQtdeIngredientes.length || arryQtdeIngredientes.length > arrayDeNomes.length) {
            alert('O campo de ingrediente está divergente!');
            $('.txtQtdeIngrediente').focus();
            return;
        }

        if (arrayDeNomes.length > arrayUnidadeIngredientes.length || arrayUnidadeIngredientes.length > arrayDeNomes.length) {
            alert('O campo de unidade está divergente!');
            $('.txtUnidadeIngrediente').focus();
            return;
        } 

        for (i = 0; i < arrayDeNomes.length; i++) {

            let ingrediente = {};

            ingrediente.nome       = arrayDeNomes[i];
            ingrediente.quantidade = parseInt(arryQtdeIngredientes[i]);
            ingrediente.unidade    = arrayUnidadeIngredientes[i];

            receita.ingredientes.push(ingrediente);
        }

        var nomesTags = $('.txtNomeTag').val();
        var arrayDeTags = nomesTags.split(',');

        if (arrayDeTags.length == 1 && !arrayDeTags[0]) {
            alert('O campo de tags está divergente!');
            $('.txtNomeTag').focus();
            return;
        }

        for (i = 0; i < arrayDeTags.length; i++) {

            let tag = {};
            tag.nome = arrayDeTags[i];

            receita.tags.push(tag);
        }

        $.ajax({

            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },

            type: "post",
            dataType: "text",
            url: "/Admin/Create",
            data: JSON.stringify(receita),

            success: function (retorno) {

                if (retorno) {
                    LimparCampos();
                }
            },

            error: function () {
                LimparCampos();
            }              

        });
        
    });

    $("#btnLimpar").click(function () {
        LimparCampos();
    });

    function LimparCampos() {

        $('.txtTituloReceita').val('');
        $('.txtDescrReceita').val('');
        $('.txtModoPrepReceita').val('');
        $('.txtFotoReceita').val('');
        $('.txtCategoriaReceita').val('');
        $('.txtNomeIngrediente').val('');
        $('.txtQtdeIngrediente').val('');
        $('.txtUnidadeIngrediente').val('');
        $('.txtNomeTag').val('');
    }

});