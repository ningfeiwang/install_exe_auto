# -*-coding:utf-8-*-
# import sys
# reload(sys)
# sys.setdefaultencoding('utf8')

#install choco: @powershell -NoProfile -ExecutionPolicy Bypass -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin

import os
from selenium import webdriver
import time

url = "http://www.baidu.com"
f = open("choco_install_ins.txt")
lines = f.readlines()

for line in lines:
	print line
	
	a = os.system(line)
	# time.sleep(15)
	# print k[0].split('\n')[0]
	if a == 0:
		print "install success"
		browser = webdriver.Chrome()
		browser.get(url)
		browser.quit()	
	else:
		print "install fail"

	log = open('log.txt', 'a')
	log.write(line)
	log.write(str(a))
	log.write('\n')
	log.close()
	