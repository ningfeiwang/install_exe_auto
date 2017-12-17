#!/usr/bin/env python
# encoding: utf-8

SERVER_IP="http://localhost:8888"

import argparse
import requests

parser = argparse.ArgumentParser(description="calculate X to the power of Y")
group = parser.add_mutually_exclusive_group()
group.add_argument("-v", "--verbose", action="store_true")
group.add_argument("-q", "--quiet", action="store_true")
parser.add_argument("-t", type=int, help="the base")
parser.add_argument("-p", type=str , default="", help="the exponent")
args = parser.parse_args()

last_argument = args.p
print last_argument
if args.t == 0: #url
	r = requests.get(SERVER_IP+ "/openurl?arg=" +last_argument)
	print r.text
elif args.t == 0:#shutdown
	r = requests.get(SERVER_IP+ "/shutdown")
	print r.text
elif args.t == 1:#reboot
	r = requests.get(SERVER_IP+ "/reboot")
	print r.text
elif args.t == 2:#runas
	r = requests.get(SERVER_IP+ "/runas?arg=" +last_argument)
	print r.text
elif args.t == 3: #bakeup
	r = requests.get(SERVER_IP+ "/backupstart?arg=" +last_argument)
	print r.text

elif args.t == 4: #resume backup
	r = requests.get(SERVER_IP+ "/backupresum?arg=" +last_argument)
	print r.text