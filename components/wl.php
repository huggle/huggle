<?
//this is a tool for whitelist management used by huggle since 2.1.15
//
//predefined text

error_reporting(E_ALL & ~E_STRICT & ~E_NOTICE);
ini_set('display_errors','1');

define (path, "/home/petrb/public_html/huggle/");

if ($_GET['displaysource'] == "true")
{
highlight_file(path . "wl.php");
die ( 0 );
}

$messageok = false; // this is supposed to be true when successfuly done
$starttime = microtime( true );
$limitsize = 2000000;
$wp=$_GET['wp'];
$action=$_GET['action'];
if (isset($_POST['action']))
{
    // attempt to get it over post
    $wp=$_POST['wp'];
    $action=$_POST['action'];
}


if (isset($_POST['wl']))
{
    $data=$_POST['wl'];
}

function usagelog($log_text)
{
    $file_log = fopen( "logs.txt", 'a' );
    fwrite ( $file_log, $log_text );
    fclose ( $file_log );
    // done
    return 2; 
}

function check_name($wikiname)
{
    //check if the wiki is a valid wiki
    // yes it is dumb but it can be improved any time ;)
    switch ($wikiname)
    {
        case "en.wikipedia":
            return "en";
        case "en":
            return "en";
        case "test2":
            return "test2";
        case "fr":
        case "fr.wikipedia":
            return "fr";
        case "de":
        case "de.wikipedia":
            return "de";
        case "ca":
        case "ca.wikipedia":
            return "ca";
        case "pt":
        case "pt.wikipedia":
            return "pt";
        case "es":
        case "es.wikipedia":
            return "es";
        case "ko":
        case "ko.wikipedia":
            return "ko";
        case "nl":
        case "nl.wikipedia":
            return "nl";
        case "no":
        case "no.wikipedia":
            return "no";
        case "hi":
        case "hi.wikipedia":
            return "hi";
        case "ru":
        case "ru.wikipedia":
            return "ru";
        case "sv":
        case "sv.wikipedia":
            return "sv";
        case "vi":
        case "vi.wikipedia":
            return "vi";
        case "ja":
        case "ja.wikipedia":
            return "ja";
        case "sv":
        case "sv.wikipedia":
            return "sv";
        case "te":
        case "te.wikipedia":
            return "te";
        case "bg":
        case "bg.wikipedia":
            return "bg";
        case "vl":
        case "vl.wikipedia":
            return "vl";
        case "km":
        case "km.wikipedia":
            return "km";
        case "zh":
        case "zh.wikipedia":
            return "zh";
        case "simple.wikipedia":
            return "simp";
        case "simple":
            return "simple";
        case "or":
        case "or.wikipedia":
            return "or";
        case "test wiki":
        case "test.wikipedia":
            return "test_w";

    }
    return "nowiki";
}

function isvalid_action ( $id )
{
    switch ( $id )
    {
    case "edit":
    case "display":
    case "read":

            return true;
    }
    return false;
}

if ($action == "" or $action == null or ( isvalid_action ( $action ) == false))
{    
    //this variable must be set but it's not
    // print readme file and exit
    include ( path . "header" );
    echo "Error no action was given, this file is used internally by huggle<!-- failed s1 -->";
    include ( path . "readme" );
    include ( path . "footer" );
    die (1);
}

if ($action == "display" )
{
    //someone want to see the content
    if ( $wp == "" or !isset ( $wp ))
    {
        echo "You need to specify the wp you want to see";
        die ( 1 );
    
    }
    $wiki =  check_name( $wp );

    if ( $wiki == "nowiki" )
    {
        echo "Wrong name";
        die ( 1 );
    }

    // get a header
    include ( path . "header");
    $filename = path . "db_" . $wiki . ".txt";

        //check size
    if ( filesize ( $filename ) == 0 )
    {
        //this should never happen
        echo "File doesn't contain data";
        die ( 0 );
    }
        
    $file = fopen ( $filename, "r" );
    $fd = fread ( $file, filesize( $filename ) ); 
    $whitelist = preg_split ( "/\|/", $fd );
    echo "List of all users in the whitelist for wiki:";
    echo '<table border="1">';
    $current_id = 0;
        while ( $current_id < count ( $whitelist ) )
    {
        echo "<tr><td>$whitelist[$current_id]</td></tr>";
        $current_id = $current_id + 1;
    }    
    echo "</table>";
    $finisht = microtime ( true ) - $starttime;
        echo "<font class=stat>Done in $finisht sec</font>";    
    // it's all folks let's close all
    fclose ( $file );
    include ( path . "footer" );
    die ( 0 );
}

if ($action == "edit")
{
    //user wants to edit it
    if ($wp == "" or $wp == null or $data == "" or $data == null)
        {
              // wrong name
               echo "Error no data!<!-- failed s4 -->";
               die (1);
    } else
    {
        if ( strlen (  $data  ) > $limitsize )
        {
            usagelog ( "$wp size exceeded on " . date ( "F j, Y, g:i a" ) );
            die (1);
        }
        $wiki = check_name ( $wp );
        if ( $wiki == "nowiki" )
        {
            echo "Wrong name of wiki<!-- fail -->";
            die (1);
        }

        $filename = path . "db_" . $wiki . ".txt";
        $handle = fopen($filename, "w");
        fwrite( $handle , $data, strlen($data) );
        fclose( $handle );
        echo "Written";
        usagelog ( "$wp was updated on " . date ( "F j, Y, g:i a" ) .  " size: " . strlen($data) . "\n" );
        die ( 0 ); // done
    }
}

if ($action == "read")
{
    if ($wp == "" or $wp == null)
    {
        // wrong name
        echo "Error no wiki!<!-- failed s2 -->";
        die (1);
    } else
    {
        $wiki = check_name( $wp );
        if ($wiki == "nowiki")
        {
            echo "Wrong name<! -- failed s4 -->";
            die ( 8 ); //nothing else to do
        }
            echo "<!-- list -->";
                $filename = path . "db_" . $wiki . ".txt";
            $handle = fopen($filename, "r");
            if (filesize($filename) > 0)
            {
                $contents = fread($handle, filesize($filename));
            }else
            {
                $contents = "<!-- failed s4 -->";
            }
                echo $contents;
            echo "<!-- list -->";
            fclose($handle);    
            die ( 0 );
    }
}
?>

