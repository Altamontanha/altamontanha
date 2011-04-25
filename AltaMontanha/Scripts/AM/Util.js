///<reference path="/Scripts/jquery-1.4.4-vsdoc.js">

var j = jQuery.noConflict();

/*
* Autor.......: Otaviano Montes Zibetti
* Descrição...: Configura um campo de data com e 
*								seu respectivo calendário.
* Data........: 14/09/2010
* Ex..........: ConfigurarHoverCalendario();
*				ConfigurarCalendario("txtDataNascimento");
*/
function ConfigurarCalendario(txtId) 
{
	j(txtId).mask("99/99/9999");

	j(txtId).datepicker(
	{
		showOn: 'button',
		buttonImageOnly: true,
		changeMonth: true,
		changeYear: true
	});
}
/*
* Autor.......: Regiane Melo
* Descrição...: Configura o hover da imagem do calendario.
* Data........: 14/09/2010
*/
function ConfigurarHoverCalendario()
{
	j(".ui-datepicker-trigger").mouseover(function ()
	{
		j(this).addClass("ui-datepicker-trigger-hover");
	}
	).mouseout(function () 
	{
		j(this).removeClass("ui-datepicker-trigger-hover");
	});
}

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