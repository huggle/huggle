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
	// Create a bar
	public function Create() {
		parent::CreateButton ("revert-and-warn", "raw", "diff-revert-warn", Core::GetMessage('main-revert-and-warn'));
		parent::CreateButton ("next-edit", "next", "diff-next", Core::GetMessage('main-tip-next'));
		parent::CreateButton ("revert", "rv", "diff-revert", Core::GetMessage('main-tip-revert'));
		parent::CreateButton ("notice", "notice", "diff-notice", Core::GetMessage('main-tip-notice'));
		parent::CreateButton ("warn", "warning", "diff-warn", Core::GetMessage('main-tip-warn')); 
		parent::CreateButton ("stop", "cancel", "cancel-all", Core::GetMessage('main-tip-cancel'));
		parent::CreateButton ("undo", "restore", "undo", Core::GetMessage('main-tip-undo'));
		return 0;
	}
}
