//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "hugglequeuefilter.h"

HuggleQueueFilter::HuggleQueueFilter(HuggleQueue *Parent)
{
    this->parent = Parent;
    QueueName = "default";
    this->IgnoreBots = true;
    this->IgnoreFriends = true;
    this->IgnoreIP = false;
    this->IgnoreMinor = false;
    this->IgnoreNP = false;
    this->IgnoreUsers = false;
}

bool HuggleQueueFilter::Matches(WikiEdit edit)
{
    if (edit.TrustworthEdit && IgnoreFriends)
    {
        return false;
    }
    if (edit.Minor && IgnoreMinor)
    {
        return false;
    }
    if (edit.NewPage && IgnoreNP)
    {
        return false;
    }
    if (edit.Bot && IgnoreBots)
    {
        return false;
    }
    return true;
}

