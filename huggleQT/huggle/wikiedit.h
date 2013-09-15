//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#ifndef WIKIEDIT_H
#define WIKIEDIT_H

#include <QString>
#include "wikiuser.h"
#include "wikipage.h"

class WikiEdit
{
public:
    WikiEdit();
    //! Page that was changed by edit
    WikiPage *Page;
    //! User who changed the page
    WikiUser *User;
    bool Minor;
    bool Bot;
    bool NewPage;
    int Size;
};

#endif // WIKIEDIT_H
