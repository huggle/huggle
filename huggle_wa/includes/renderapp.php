<?php
//This is a source code or part of Huggle project
//
//This file contains code for index page
//last modified by Petrb

//Copyright (C) 2011-2012 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
/* Huggle WA - app/renderapp.php */

class Html {

private static function Header() {
	include "html/template_header";
}
private static function Menu() {
	global $hgwa_Username;
	echo "<table align=right border=0><tr><td></td><td>";
	if ($hgwa_Username === false) {
		echo '<a href="index.php?action=login">' . Core::GetMessage("login").'</a>';
	} else
	{
		echo "$hgwa_Username" . '<a href="index.php?action=logoff">' . Core::GetMessage("logout") . "</a>";
	}	
	echo "</td></tr></table>";
}

private static function Footer() {
	global $hgwa_HtmlTitle;
	include "html/template_footer";
}

public static function LoadContent() {
	self::Header();
	self::Menu();
	$selectedwiki = "<p>" . Core::GetMessage("select_wiki") . "</p>"; // Function not implemented
	echo $selectedwiki;
	Html::Footer();
	return 0;
}
}
