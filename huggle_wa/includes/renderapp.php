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
/* Huggle WA - app/renderapp.php */

if ( !defined( 'HUGGLE' ) ) {
	echo "This is a part of huggle wa, unable to load config";
	die (1);
}

class Html {

private static function Header() {
	include "html/template_header";
}
private static function Menu() {
	global $hgwa_Username;
	echo "<tr><td colspan=2><table align=right border=0 height='100%'><tr><td>\n</td><td>";
	if ($hgwa_Username === false) {
		echo '<a href="index.php?action=login">' . Core::GetMessage("login").'</a>';
	} else
	{
		echo "$hgwa_Username" . '<a href="index.php?action=logoff">' . Core::GetMessage("logout") . "</a>";
	}	
	echo "</td></tr>\n</table>\n</td>\n</tr>";
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
	// queue
	echo "<tr>\n<td width=\"" . $hgwa_QueueWidth . "\" height=\"100%\" style=\"border-right:1px solid black; background-color:white;\">" . Core::GetMessage("queue") . "\n</td>";

	// body
	echo "<td>";
	if ( $hgwa_Username === false ) {
		echo "<font size=4>". Core::GetMessage("anon") ."</font>";	
	}
	echo "</td>\n</tr>";

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
