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
		if (!isset($hgwa_Message)) {
			echo "undefinded translation";
		}
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

	public static function isLanguage( $code ) {
		switch ($code) {
			case "en":
			case "de":
			case "jp":
			case "cs":
			case "ar":
			case "bg":
			case "es":
			case "fa":
			case "fr":
			case "hi":
			case "it":
			case "kn":
			case "ml":
			case "mr":
			case "nl":
			case "no":
			case "oc":
			case "pt":
			case "ru":
				return true;
		}
		return false;
	}
	public static function getOverrides() {
		global $hgwa_Language, $hgwa_Skin;
		if ( isset ($_GET['uselang']) ) {
			if (Core::isLanguage($_GET['uselang'])) {
				$hgwa_Language = $_GET['uselang'];
				self::LoadLanguage();
			}
			Core::Info("Override for language is triggered");
		}
	}

	private static function getAction() {
		global $hgwa_Username;
		if ( isset ($_GET['action'] ) ) {
			switch($_GET['action'])  {
				case "logout":
					Core::$action = "logout";
					Core::Logout();	
					break;
				case "login":
					Core::$action = "login";
					Core::Login();
					break;
				case "options":
					Core::$action = "options";
					break;
				case "about":
					Core::$action = "about";
					break;
				default:
					Core::Info('unknown "$action"' );
					break;
			}
		}
	}
	
	public static function Auth() {
		global $hgwa_Username;
		if ($hgwa_Username == null) {
			Html::$_page = Core::GetMessage( 'anon' );
			return false;
		}
		return true;
	}
	
	private static function Logout() {
		global $hgwa_Username;
		$hgwa_Username = null;
		Html::$_page = Core::GetMessage( 'logout-done' );
	        return;	
	}

	private static function Login() {
		global $hgwa_Username;
		Html::ChangeTitle( Core::GetMessage( 'title-login' ) );
		Core::Info ( "User login" );
		if ( $hgwa_Username != null ) {
			Core::Info( "User is already logged in" );
			Html::$_page = Core::GetMessage( 'loggedfail' );
			return 0;
		}
		return 0;
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
		Core::Info ( "Started huggle version " . $hgwa_Version . " languages loaded, loading other files\n" );
		include("includes/loadwikis.php");
		include("includes/functions.php");
		include("includes/parse-rc.php");
		include("includes/renderapp.php");
		Core::getOverrides();
		Core::getAction();
		if (Core::$action == null) {
			Core::Auth();
		}
		Core::Info( "All include files were loaded, initialisation is done\n" );
	}

	// Load a web page
	public static function LoadContent() {
		global $hgwa_HtmlTitle, $hgwa_Version;
		Html::LoadContent();
		Core::Info( "Page generated ok" );
		return 1;
	}
}
