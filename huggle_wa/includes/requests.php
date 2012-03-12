<?php
//This is a source code or part of Huggle project
//
//This file contains code for requests
//last modified by Addshore

//Copyright (C) 2011-2012 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

/*
 *  Developers of the origional code that these functions were based upon:
 *      Cobi    - [[User:Cobi]]     - Wrote the http class and some of the wikipedia class
 *      Chris   - [[User:Chris_G]]  - Wrote the most of the wikipedia class
 *      Fale    - [[User:Fale]]     - Polish, wrote the extended and some of the wikipedia class
 *      Kaldari - [[User:Kaldari]]  - Submitted a patch for the imagematches function
 *      Gutza   - [[User:Gutza]]    - Submitted a patch for http->setHTTPcreds()
 *      Addshore- [[User:Addshore]] - Altered all code for use in huggle-wa
 **/

class wikipedia {
    private $http;
    private $token;
    private $ecTimestamp;
    public $url;

    function __construct ($url='http://en.wikipedia.org/w/api.php',$hu=null,$hp=null) {
        $this->http = new http;
        $this->token = null;
        $this->url = $url;
        $this->ecTimestamp = null;
        if ($hu!==null)
            $this->http->setHTTPcreds($hu,$hp);
    }
	
	/**
     * Sends a query to the api.
     * @param $query The query string.
     * @param $post POST data if its a post request (optional).
     * @return The api result.
     **/
    function query ($query,$post=null) {
        if ($post==null)
            $ret = $this->http->get($this->url.$query);
        else
            $ret = $this->http->post($this->url.$query,$post);
        return unserialize($ret);
    }

    /**
     * Gets the content of a page. Returns false on error.
     * @param $page The wikipedia page to fetch.
     * @param $revid The revision id to fetch (optional)
     * @return The wikitext for the page.
     **/
    function getpage ($page,$revid=null,$detectEditConflict=false) {
        $append = '';
        if ($revid!=null)
            $append = '&rvstartid='.$revid;
        $x = $this->query('?action=query&format=php&prop=revisions&titles='.urlencode($page).'&rvlimit=1&rvprop=content|timestamp'.$append);
        foreach ($x['query']['pages'] as $ret) {
            if (isset($ret['revisions'][0]['*'])) {
                if ($detectEditConflict)
                    $this->ecTimestamp = $ret['revisions'][0]['timestamp'];
                return $ret['revisions'][0]['*'];
            } else
                return false;
        }
    }
	
}//end wikipedia class

class http {
    private $ch;
    private $uid;
    public $cookie_jar;

    function data_encode ($data, $keyprefix = "", $keypostfix = "") {
        assert( is_array($data) );
        $vars=null;
        foreach($data as $key=>$value) {
            if(is_array($value))
                $vars .= $this->data_encode($value, $keyprefix.$key.$keypostfix.urlencode("["), urlencode("]"));
            else
                $vars .= $keyprefix.$key.$keypostfix."=".urlencode($value)."&";
        }
        return $vars;
    }

    function __construct () {
        $this->ch = curl_init();
		//Generate a random number for the cookie, if it exists do it again
		do {
			$this->uid = dechex(rand(0,99999999));
		}while(file_exists('/tmp/huggle.cookies.'.$this->uid.'.dat') == true)
        curl_setopt($this->ch,CURLOPT_COOKIEJAR,'/tmp/huggle.cookies.'.$this->uid.'.dat');
        curl_setopt($this->ch,CURLOPT_COOKIEFILE,'/tmp/huggle.cookies.'.$this->uid.'.dat');
        curl_setopt($this->ch,CURLOPT_MAXCONNECTS,100);
        curl_setopt($this->ch,CURLOPT_CLOSEPOLICY,CURLCLOSEPOLICY_LEAST_RECENTLY_USED);
        $this->cookie_jar = array();
    }

    function post ($url,$data) {
        $time = microtime(1);
        curl_setopt($this->ch,CURLOPT_URL,$url);
        curl_setopt($this->ch,CURLOPT_USERAGENT,'huggle_wa');
        $cookies = null;
        foreach ($this->cookie_jar as $name => $value) {
            if (empty($cookies))
                $cookies = "$name=$value";
            else
                $cookies .= "; $name=$value";
        }
        if ($cookies != null)
            curl_setopt($this->ch,CURLOPT_COOKIE,$cookies);
        curl_setopt($this->ch,CURLOPT_MAXREDIRS,10);
        curl_setopt($this->ch, CURLOPT_HTTPHEADER, array('Expect:'));
        curl_setopt($this->ch,CURLOPT_RETURNTRANSFER,1);
        curl_setopt($this->ch,CURLOPT_TIMEOUT,30);
        curl_setopt($this->ch,CURLOPT_CONNECTTIMEOUT,10);
        curl_setopt($this->ch,CURLOPT_POST,1);
        curl_setopt($this->ch,CURLOPT_POSTFIELDS, $data);
        $data = curl_exec($this->ch);
        return $data;
    }

    function get ($url) {
        $time = microtime(1);
        curl_setopt($this->ch,CURLOPT_URL,$url);
        curl_setopt($this->ch,CURLOPT_USERAGENT,'huggle_wa');
        $cookies = null;
        foreach ($this->cookie_jar as $name => $value) {
            if (empty($cookies))
                $cookies = "$name=$value";
            else
                $cookies .= "; $name=$value";
        }
        if ($cookies != null)
            curl_setopt($this->ch,CURLOPT_COOKIE,$cookies);
        curl_setopt($this->ch,CURLOPT_MAXREDIRS,10);
        curl_setopt($this->ch,CURLOPT_HEADER,0);
        curl_setopt($this->ch,CURLOPT_RETURNTRANSFER,1);
        curl_setopt($this->ch,CURLOPT_TIMEOUT,30);
        curl_setopt($this->ch,CURLOPT_CONNECTTIMEOUT,10);
        curl_setopt($this->ch,CURLOPT_HTTPGET,1);
        $data = curl_exec($this->ch);
        return $data;
    }

    function setHTTPcreds($uname,$pwd) {
        curl_setopt($this->ch, CURLOPT_HTTPAUTH, CURLAUTH_BASIC);
        curl_setopt($this->ch, CURLOPT_USERPWD, $uname.":".$pwd);
    }

    function __destruct () {
        curl_close($this->ch);
        @unlink('/tmp/huggle.cookies.'.$this->uid.'.dat');
    }
	
}//end of http class

?>