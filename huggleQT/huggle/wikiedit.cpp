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
    this->Status = StatusNone;
    this->CurrentUserWarningLevel = WarningLevelNone;
    this->OwnEdit = false;
    this->EditMadeByHuggle = false;
    this->TrustworthEdit = false;
    this->RollbackToken = "";
    this->PostProcessing = false;
    this->ProcessingQuery = NULL;
}

WikiEdit::WikiEdit(const WikiEdit &edit)
{
    this->User = NULL;
    this->Page = NULL;
    this->Bot = edit.Bot;
    this->Minor = edit.Minor;
    this->NewPage = edit.NewPage;
    if (edit.Page != NULL)
    {
        this->Page = new WikiPage(edit.Page);
    }
    this->Size = edit.Size;
    if (edit.User != NULL)
    {
        this->User = new WikiUser(edit.User);
    }
    this->Diff = edit.Diff;
    this->OldID = edit.OldID;
    this->CurrentUserWarningLevel = edit.CurrentUserWarningLevel;
    this->Summary = edit.Summary;
    this->Status = edit.Status;
    this->RollbackToken = edit.RollbackToken;
    this->OwnEdit = edit.OwnEdit;
    this->EditMadeByHuggle = edit.EditMadeByHuggle;
    this->TrustworthEdit = edit.TrustworthEdit;
    this->PostProcessing = false;
    this->ProcessingQuery = NULL;
}

WikiEdit::WikiEdit(WikiEdit *edit)
{
    this->User = NULL;
    this->Page = NULL;
    this->Bot = edit->Bot;
    this->Minor = edit->Minor;
    this->NewPage = edit->NewPage;
    if (edit->Page != NULL)
    {
        this->Page = new WikiPage(edit->Page);
    }
    this->Size = edit->Size;
    if (edit->User != NULL)
    {
        this->User = new WikiUser(edit->User);
    }
    this->OldID = edit->OldID;
    this->CurrentUserWarningLevel = edit->CurrentUserWarningLevel;
    this->Summary = edit->Summary;
    this->Diff = edit->Diff;
    this->OwnEdit = edit->OwnEdit;
    this->Status = edit->Status;
    this->RollbackToken = edit->RollbackToken;
    this->EditMadeByHuggle = edit->EditMadeByHuggle;
    this->TrustworthEdit = edit->TrustworthEdit;
    this->PostProcessing = false;
    this->ProcessingQuery = NULL;
}

WikiEdit::~WikiEdit()
{
    delete this->ProcessingQuery;
    delete this->User;
    delete this->Page;
}

bool WikiEdit::FinalizePostProcessing()
{
    if (!this->PostProcessing)
    {
        return false;
    }
    if (this->ProcessingQuery == NULL)
    {
        return false;
    }
    // check if api was processed
    if (!this->ProcessingQuery->Processed())
    {
        return false;
    }

    if (this->ProcessingQuery->Result->Failed)
    {
        // whoa it ended in error, we need to get rid of this edit somehow now
        delete this->ProcessingQuery;
        this->ProcessingQuery = NULL;
        this->PostProcessing = false;
        return true;
    }

    // so we can parse the data from query now

    QDomDocument d;
    d.setContent(this->ProcessingQuery->Result->Data);
    QDomNodeList l = d.elementsByTagName("rev");
    // get last id
    if (l.count() > 0)
    {
        QDomElement e = l.at(0).toElement();
        if (e.nodeName() == "rev")
        {
            // check if this revision matches our user
            if (e.attributes().contains("user"))
            {
                if (e.attribute("user") == this->User->Username)
                {
                    if (e.attributes().contains("rollbacktoken"))
                    {
                        // finally we managed to get a rollback token
                        this->RollbackToken = e.attribute("rollbacktoken");
                    }
                }
            }
        }
    }

    delete this->ProcessingQuery;
    this->ProcessingQuery = NULL;
    this->Status = StatusPostProcessed;
    this->PostProcessing = false;
    return true;
}

void WikiEdit::PostProcess()
{
    if (this->PostProcessing)
    {
        return;
    }
    this->PostProcessing = true;
    this->ProcessingQuery = new ApiQuery();
    this->ProcessingQuery->SetAction(ActionQuery);
    this->ProcessingQuery->Parameters = "prop=revisions&rvprop=ids|user&rvlimit=1&rvtoken=rollback&titles=" +
            QUrl::toPercentEncoding(this->Page->PageName);
    this->ProcessingQuery->Process();
}

bool WikiEdit::IsPostProcessed()
{
    if (this->Status == StatusPostProcessed)
    {
        return true;
    }
    return false;
}

