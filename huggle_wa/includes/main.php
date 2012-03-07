<?
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
//GNU General Public License for more details

if ( !defined( 'HUGGLE' ) ) {
	echo "This is a part of huggle wa";
	die (1);
}


class Core {
	// Return a translated text
	public static function GetMessage ( $text ) {
		return $hgwa_Message[$text];
	}
	
	public static function LoadLanguage () {
		global $hgwa_DefaultLoc, $hgwa_Locals, $hg$hgwa_Language;
		switch ($hgwa_Language) {
		case 'en':
		case 'cs':
			include ( "$hgwa_Locals" . $hgwa_Language . "_main.php" );
			return true;
		}
		include ( "$hgwa_Locals" . 'en.php' );
		return true;
	}

	public static function Initialise() {
		Core::LoadLanguage();
	}

	// Load a web page
	public static function LoadContent() {
		include ("html/template_header");
		loadcontent();
		include ("html/template_footer");
		return 1;
	}
}
