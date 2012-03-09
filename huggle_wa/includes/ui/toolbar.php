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

require ( "includes/ui/generictool.php" );

class ToolBar extends Tool {
	public function Display() {
		self->Render();
	}
	public function Create() {
		self->CreateButton ("revert-and-warn", "huggle_raw", "", Core::Mesage('main-revert-and-warn') );
		self->CreateButton ("revert", "huggle-rv", "", Core::Message(''));
		return 0;
	}
}
