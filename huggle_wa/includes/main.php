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
	echo "This is a part of huggle wa, unable to load config";
	die (1);
}

include ("includes/variables.php");

class Core {
	public static $action = null;
	// Return a translated text
	public static function GetMessage ( $text ) {
		global $hgwa_Message, $hgwa_Debugging;
		return $hgwa_Message["$text"];
	}
	
	public static function LoadLanguage () {
		global $hgwa_Debugging, $hgwa_DefaultLoc, $hgwa_Locals, $hgwa_Message, $hgwa_Language;
		switch ($hgwa_Language) {
		case 'en':
		case 'cs':
			include ( "$hgwa_Locals" . $hgwa_Language . "_main.php" );
			return true;
		}
		if ( $hgwa_Debugging ) {
			echo "<!-- Error: Invalid language -->\n";
		}
		include ( "$hgwa_Locals" . 'en.php' );
		return true;
	}

	private static function getAction() {
		global $hgwa_Username;
		if ( $_GET['action'] != null ) {
			switch($_GET['action'])  {
				case "logout":
					Core::$Action = "logout";
					Core::Logout();	
					break;
				case "login":
					Core::$Action = "login";
					Core::Login();
					break;
				case "options":
					Core::$Action = "options";
					break;
				case "about":
					Core::$Action = "about";
					break;
				default:
					Core::Info('unknown "$action"' );
					break;
			}
		}
	}
		
	private static function Logout() {
		global $hgwa_Username;
		$hgwa_Username = null;
		Html::$_page = Core::Message( 'logout-done' );
	        return;	
	}

	private static function Login() {
		global $hgwa_Username;
		if ( $hgwa_Username != null ) {
			Core::Info( "User is already logged in" );
			Html::$_page = Core::Message( 'loggedfail' );
			Html::ChangeTitle( Core::Message( 'title-login' ) );	
			return 0;
		}
		Core::Info ( "User login" );

	}

	public static function Info($data) {
		global $hgwa_Debugging;
		if ( $hgwa_Debugging == false ) {
			return 0;
		}
		echo "<!-- Message: $data -->\n";
	}

	// Initialise
	public static function Initialise() {
		global $hgwa_Debugging, $hgwa_Version;
		Core::LoadLanguage();
		Core::Info ( "<!-- Started huggle version " . $hgwa_Version . " languages loaded, loading other files -->\n" );
		include("includes/loadwikis.php");
		include("includes/functions.php");
		include("includes/parse-rc.php");
		include("includes/renderapp.php");
		Core::getAction();
		Core::Info( "<!-- All include files were loaded, initialisation is done -->\n" );
	}

	// Load a web page
	public static function LoadContent() {
		global $hgwa_HtmlTitle, $hgwa_Version;
		Html::LoadContent();
		Core::Info( "Page generated ok" );
		return 1;
	}
}
