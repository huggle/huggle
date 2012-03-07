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
/* Huggle WA - app/parse-rc.php 
	$ch2 = curl_init();
	$curlurl2 = $wikiurl1."/api.php?action=query&list=recentchanges&format=xml";

	curl_setopt($ch2, CURLOPT_URL, $curlurl2);
	curl_setopt($ch2, CURLOPT_HEADER, 0);
	curl_setopt($ch2, CURLOPT_RETURNTRANSFER, 1);
	
	$rcxml = curl_exec($ch2);
	
	curl_close($ch2);
	
	$rcdata = new SimpleXMLElement($rcxml);
	
	$rcid = $rcdata->query->recentchanges->rc[0]->attributes()->rcid;
	$rctitle = $rcdata->query->recentchanges->rc[0]->attributes()->title;
	$revid = $rcdata->query->recentchanges->rc[0]->attributes()->revid;
	$oldrevid = $rcdata->query->recentchanges->rc[0]->attributes()->old_revid;
	$timestamp = $rcdata->query->recentchanges->rc[0]->attributes()->timestamp;
	
	$rcarray = array(
	"rcid" => $rcid,
	"rctitle" => $rctitle,
	"revid" => $revid,
	"oldrevid" => $oldrevid,
	"timestamp" => $timestamp
);
	fix me
 */	
