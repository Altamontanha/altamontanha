///<reference path="jquery.vsdoc.js">

var master = '#form_';

// Função utilizada para pesquisa usando o plugin NyroModal
// Parâmetros(id do botão, endereço da pesquisa, controle para busca, altura janela, largura janela)
function ConfigurarModal(botao, link, altura, largura) 
{
    j(master + botao).click(function (e) 
		{
        e.preventDefault();
    
        j.nyroModalManual(
				{
            url: link,
            width: altura,
            height: largura,
            forceType: 'iframe'
        });

        return false;
    });

}