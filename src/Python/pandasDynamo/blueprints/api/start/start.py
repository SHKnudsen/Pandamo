import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request

mod = Blueprint('start', __name__)
null = None

@mod.route('/')
def start_page():
    return 'PANDAMOoooo!!!!!!!!!!!!!!!!!!!!!!!!!'