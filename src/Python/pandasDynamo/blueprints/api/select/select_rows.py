import json
import pandas as pd
import sys
from flask import Blueprint
from flask import current_app as app
from flask import request

mod = Blueprint('select_rows', __name__)
null = None
@mod.route('by_match/', methods=["POST"])
def by_match():
    request_dict = request.get_json()
    jsonstr = request_dict['jsonStr']
    column = request_dict['column']
    match_str = request_dict['matchString']

    df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
    selected_rows = df[df[column].str.match(match_str,na=False)]
    df_json = selected_rows.to_json(orient='split')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response

@mod.route('by_contains/', methods=["POST"])
def by_contains():
    request_dict = request.get_json()
    jsonstr = request_dict['jsonStr']
    column = request_dict['column']
    contains_str = request_dict['containsString']
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
    selected_rows = df[df[column].str.contains(contains_str,na=False)]
    df_json = selected_rows.to_json(orient='split')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response