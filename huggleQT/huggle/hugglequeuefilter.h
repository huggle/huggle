//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#ifndef HUGGLEQUEUEFILTER_H
#define HUGGLEQUEUEFILTER_H

#include <QString>
#include "core.h"
#include "hugglequeue.h"

class HuggleQueueFilter
{
public:
    QString QueueName;
    HuggleQueue *parent;
    HuggleQueueFilter(HuggleQueue *Parent);
};

#endif // HUGGLEQUEUEFILTER_H
