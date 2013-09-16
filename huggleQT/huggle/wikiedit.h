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

enum WarningLevel
{
    WarningLevelNone,
    WarningLevel1,
    WarningLevel2,
    WarningLevel3,
    WarningLevel4,
    WarningLevel5
};

class WikiEdit
{
public:
    WikiEdit();
    WikiEdit(const WikiEdit& edit);
    WikiEdit(WikiEdit *edit);
    ~WikiEdit();
    //! Page that was changed by edit
    WikiPage *Page;
    //! User who changed the page
    WikiUser *User;
    bool Minor;
    bool Bot;
    bool NewPage;
    int Size;
    int Diff;
    int OldID;
    bool Processed;
    WarningLevel CurrentUserWarningLevel;
    QString Summary;
    //! If this is true the edit was made by huggle
    bool EditMadeByHuggle;
    //! If this is true the edit was made by some other
    //! tool for vandalism reverting
    bool TrustworthEdit;
    //! Edit was made by you
    bool OwnEdit;
};

#endif // WIKIEDIT_H
