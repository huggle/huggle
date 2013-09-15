//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "wikiedit.h"

WikiEdit::WikiEdit()
{
    this->Bot = false;
    this->User = NULL;
    this->Minor = false;
    this->NewPage = false;
    this->Size = 0;
    this->User = NULL;
    this->Diff = 0;
    this->OldID = 0;
    this->Summary = "";
}

WikiEdit::WikiEdit(const WikiEdit &edit)
{
    this->Bot = edit.Bot;
    this->Minor = edit.Minor;
    this->NewPage = edit.NewPage;
    this->Page = edit.Page;
    this->Size = edit.Size;
    this->User = edit.User;
    this->Diff = edit.Diff;
    this->OldID = edit.OldID;
    this->Summary = edit.Summary;
}

WikiEdit::WikiEdit(WikiEdit *edit)
{
    this->Bot = edit->Bot;
    this->Minor = edit->Minor;
    this->NewPage = edit->NewPage;
    this->Page = edit->Page;
    this->Size = edit->Size;
    this->User = edit->User;
    this->Diff = edit->Diff;
    this->OldID = edit->OldID;
    this->Summary = edit->Summary;
}
