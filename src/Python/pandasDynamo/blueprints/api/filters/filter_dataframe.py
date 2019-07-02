import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os
from flask import Blueprint
from flask import jsonify
from flask import current_app as app
from utillities.string_helpers import string_to_list
from utillities.exception_class import EmptyDataframe

mod = Blueprint('filter_dataframe', __name__)

# Create Dataframes
@mod.route('by_items/<string:jsonstr>/<string:items>/<int:axis>')
def by_items(jsonstr,items,axis):

    items = string_to_list(items)
    axis = int(axis)
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='index')
    df = df.filter(items=items, axis=axis)
    if df.empty:
        raise EmptyDataframe({"content": "Returned empty dataframe, check your filter index and items"}, status_code=400)
    df_json = df.to_json(orient='index')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response

# ___________Needs testing___________
@mod.route('by_regex/<string:jsonstr>/<string:items>/<int:axis>')
def by_regex(jsonstr,items,axis):
    axis = int(axis)
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='index')
    df = df.filter(regex=items, axis=axis)
    if df.empty:
        raise EmptyDataframe({"content": "Returned empty dataframe, check your filter index and items"}, status_code=400)
    df_json = df.to_json(orient='index')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response

# ___________Needs testing___________
@mod.route('by_contains/<string:jsonstr>/<string:items>/<int:axis>')
def by_contains(jsonstr,items,axis):
    axis = int(axis)
    df = pd.read_json(json.dumps(eval(jsonstr)), orient='index')
    df = df.filter(like=items, axis=axis)
    if df.empty:
        raise EmptyDataframe({"content": "Returned empty dataframe, check your filter index and items"}, status_code=400)
    df_json = df.to_json(orient='index')
    response = app.response_class(
        response=df_json,
        status=200,
        mimetype='application/json'
    )
    return response