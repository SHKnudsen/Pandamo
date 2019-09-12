import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request
import time
from flask import jsonify

mod = Blueprint('server_started', __name__)
null = None

@mod.route('has_server_started/', methods=["GET"])
def has_server_started():
    url = request.host_url
    t = time.strftime('[%d/%m/%Y %H:%M:%S]')
    response = jsonify(f'{t} Pandamo server is running locally on {url}')
    return response