'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for
'last modified by Petrb

'Copyright (C) 2011 Huggle team

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

Class ListView2

    Inherits ListView

    Sub New()
        DoubleBuffered = True
        FullRowSelect = True
        View = Windows.Forms.View.Details
        GridLines = True
        HeaderStyle = ColumnHeaderStyle.Nonclickable
    End Sub

End Class
