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
	echo "This is a part of huggle wa, error";
	die (1);
}

class Dialog {
	public $Text;
	public $Height;
	public $Width;
	public $PosX;
	public $PosY;
	public static function render() {
		$code = "<code is here>";
		Html::$_page = Html::$_page + $code;
	}

	public static function __construct( $_text, $_posx, $_posy, $_hght, $_wdth ) {
		$this->Text = $_text;
		$this->PosX = $_posx;
		$this->PosY = $_posy;
		$this->Height = $_hght;
		$this->Width = $_wdth;
	}

	public static function OnClick() {
	
	}

	public static function OnClose() {
	}
}
