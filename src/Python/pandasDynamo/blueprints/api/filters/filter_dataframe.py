import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os
from flask import Blueprint
from flask import jsonify
from flask import request
from flask import current_app as app
import numpy as np
from utillities.string_helpers import string_to_list
from utillities.exception_class import EmptyDataframe

mod = Blueprint('filter_dataframe', __name__)
null = None
# Create Dataframes
@mod.route('by_items/', methods=["POST"])
def by_items():
    request_dict = request.get_json()
    jsonstr = request_dict['jsonStr']
    items = request_dict['items']
    axis = request_dict['axis']
    items = items
    axis = int(axis)
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
    df = df.filter(items=items, axis=axis)
    print(df)
    if df.empty:
        raise EmptyDataframe({"content": "Returned empty dataframe, check your filter index and items"}, status_code=400)
    df_json = df.to_json(orient='split', date_format='iso')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response

@mod.route('by_regex/', methods=["POST"])
def by_regex():
    request_dict = request.get_json()
    jsonstr = request_dict['jsonStr']
    items = request_dict['item']
    axis = request_dict['axis']
    axis = int(axis)
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
    df = df.filter(regex=items, axis=axis)
    if df.empty:
        raise EmptyDataframe({"content": "Returned empty dataframe, check your filter index and items"}, status_code=400)
    df_json = df.to_json(orient='split')
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
    items = request_dict['item']
    axis = request_dict['axis']
    axis = int(axis)
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
    df = df.filter(like=items, axis=axis)
    if df.empty:
        raise EmptyDataframe({"content": "Returned empty dataframe, check your filter index and items"}, status_code=400)
    df_json = df.to_json(orient='split')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response