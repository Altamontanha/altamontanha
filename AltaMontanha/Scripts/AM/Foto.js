///<reference path="/Scripts/jquery-1.4.4-vsdoc.js">1

var j = jQuery.noConflict();

j(function (Top, Left, Right, Bottom)
{
	jQuery('#profileImageEditor').Jcrop(
	{
		onChange: showPreview,
		onSelect: showPreview,
		setSelect: [Top, Left, Right, Bottom],
		aspectRatio: 1
	});
});

function showPreview(coords, Width, Height) 
{
	if (parseInt(coords.w) > 0) 
	{
		j('#Top').val(coords.y);
		j('#Left').val(coords.x);
		j('#Bottom').val(coords.y2);
		j('#Right').val(coords.x2);

		var width = Width;
		var height = Height;
		var rx = 100 / coords.w;
		var ry = 100 / coords.h;

		jQuery('#preview').css(
		{
			width: Math.round(rx * width) + 'px',
			height: Math.round(ry * height) + 'px',
			marginLeft: '-' + Math.round(rx * coords.x) + 'px',
			marginTop: '-' + Math.round(ry * coords.y) + 'px'
		});
	}
}
