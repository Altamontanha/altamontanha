///<reference path="../jquery-1.4.4-vsdoc.js">

var j = jQuery.noConflict();

j(document).ready(function () 
{
	j("#destaques-wrap").easySlider(
	{
		auto: true,
		continuous: true,
		numeric: true
	});
});