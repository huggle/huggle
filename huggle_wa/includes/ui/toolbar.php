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

class ToolBar extends Tool {
	public function Display() {
		self.Render();
	}
	public function Create() {
		parent::CreateButton ("revert-and-warn", "huggle_raw", "diff-revert-warn", Core::GetMessage('main-revert-and-warn') );
		parent::CreateButton ("next-edit", "diff_next", "diff-next", Core::GetMessage('main-next') );
		parent::CreateButton ("revert", "huggle-rv", "diff-revert", Core::GetMessage('main-revert'));
		return 0;
	}
}
