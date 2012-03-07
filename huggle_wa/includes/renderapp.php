<?php
//This is a source code or part of Huggle project
//
//This file contains code for index page
//last modified by IWorld

//Copyright (C) 2011-2012 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
/* Huggle WA - includes/renderapp.php */

if ( !defined( 'HUGGLE' ) ) {
	echo "This is a part of huggle wa, unable to load config";
	die (1);
}

class Html {

private static function Header() {
	$hgwa_HtmlTitle = "Huggle WA"; // only temporary
	include "html/template_header";
}
private static function Menu() {
	global $hgwa_Username;
	echo "\n<div class='menu'><div class='menubuttons'><!-- Menu -->Menu</div><div class='menulogin'>";
	if ($hgwa_Username === false) {
		echo '<a href="index.php?action=login">' . Core::GetMessage("login").'</a>';
	} else
	{
		echo "$hgwa_Username" . '<a href="index.php?action=logoff">' . Core::GetMessage("logout") . "</a></div>";
	}	
	echo "</div>";
}

private static function ToolBar() {
	global $hgwa_Debugging;
	if ( $hgwa_Debugging ) {
			echo "<!-- Tool bar -->\n";
		}
}

private static function Statsbar() {
	global $hgwa_Debugging;
	if ( $hgwa_Debugging ) {
			echo "<!-- Status bar -->\n";
		}
}

private static function Content() {
	global $hgwa_Username, $hgwa_QueueWidth;
	echo "<div class=\"interface\">";
	// queue
	echo "<div class=\"queue\">" . Core::GetMessage("queue") . "\n</div>";
	// body
	echo "<div class=\"content\">";
	if ( $hgwa_Username === false ) {
		echo Core::GetMessage("anon");	
	}
	echo "</div></div>";

}

private static function Footer() {
	global $hgwa_HtmlTitle;
	include "html/template_footer";
}

public static function LoadContent() {
	self::Header();
	self::Menu();
	self::ToolBar();
	self::Content();
	self::Statsbar();
	Html::Footer();
	return 0;
}
}
