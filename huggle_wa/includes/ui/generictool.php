<?
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

class Button {
	public $Name;
	public $Action;
	public $Picture;
	public $Tip;
	public function __construct($_name, $_url, $_picture, $_tip) {
		$this->Name = $_name;
		$this->Picture = $_picture;
		$this->Tip = $_tip;
		$this->Action = $_url;
	}
}

class Label {
	public $Text;
}	

class Tool {
	public $Labels = array();
	public $Name = null;
	public $Buttons = array();
	public function Render () {
		echo "<table><tr>";
		echo "</tr></table>";
		return;
	}

	public function CreateButton( $name, $url, $pict, $tip ) {
		$this->Buttons[$name] = new Button ( $name, $url, $pict, $tip ) );
		return true;
	}

	public function CreateLabel($name) {
		return false;
	}
}
