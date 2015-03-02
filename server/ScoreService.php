<?php

class ScoreService {
  
  const SECRET_KEY = "YOUR-SECRET-KEY";
  
  private $dao;
  
  public function __construct($dao) {
    if (!$dao) {
      throw new Exception('No dao found');
    }
    $this->dao = $dao;
  }
  
  public function addScore($score) {
    $this->dao->addScore($score);
  }
  
  public function getTopScores24hours($gameVersion) {
    $scores = $this->dao->getTopScores24hours();
    $scores = $this->fixOldVersionGameScores($scores, $gameVersion);
    return $scores;
  }
  
  public function getTopScoresAllTime($gameVersion) {
    $scores = $this->dao->getTopScoresAllTime();
    $scores = $this->fixOldVersionGameScores($scores, $gameVersion);
    return $scores;
  }
  
  private function fixOldVersionGameScores($scores, $gameVersion) {
    if (version_compare($gameVersion, "1.14", "<")) {
      foreach ($scores as $score) {
        
        //if client version doesnt have character 6 return 0 for that player
        if ($score->characterplayed > 5) {
          $score->characterplayed = 0;
        }
      }
    }
    return $scores;
  }
  
  public function formatScoresArray($scores) {
    $scoresArray = array();
    
    //name,score,device,isocountrycode,characterplayed
    foreach($scores as $score) {
      $scoresArray[] = array($score->name, $score->score, $score->device, $score->isocountrycode, $score->characterplayed);
    }
    
    return $scoresArray;
  }
  
  public function checkHash($name, $score, $hash) {
    $real_hash = md5($name . $score . self::SECRET_KEY); 
    return $real_hash == $hash;
  }
  
  public function getCountryCodeFromRemoteAddress($remoteAdress) {
    $url_track = "http://ipinfo.io/" . $remoteAdress . "/country"; 
    return @file_get_contents($url_track);
  }
  
  public function getDeviceFromUserAgent($userAgent) {
    $device = "webplayer";

    if(strpos(strtolower($userAgent),"windows phone") !== false){
      $device = "windowsphone";
    } else if (strpos(strtolower($userAgent),"android") !== false) {
      $device = "android";
    } else if (strpos(strtolower($userAgent),"ipad") !== false) {
      $device = "ios";
    } else if (strpos(strtolower($userAgent),"iphone") !== false) {
      $device = "ios";
    } else if (strpos(strtolower($userAgent),"ipod") !== false) {
      $device = "ios";
    } else if (strpos(strtolower($userAgent),"cfnetwork") !== false) {
      $device = "ios";
    }
    
    return $device;
  }
}