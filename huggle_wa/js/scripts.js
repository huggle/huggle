$(document).ready(function() {
    $("#logindialog").dialog({
	title: 'Login',
	width: '300',
	height: '250',
	resizable: false,
	modal: true,
	buttons: [
		{
			text:'Login',
			click: function() {
				alert("Info: at the moment you can\'t login into Huggle WA.");
			}
		},
		{
			text:'Cancel',
			click: function() {
				$(this).dialog('close');
			}
		}
	]
	});
});