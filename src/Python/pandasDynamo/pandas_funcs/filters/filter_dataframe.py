import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os

def by_items(df,items, axis):
    try:
        df = df.filter(items=items, axis=axis)
        return df
    except Exception as e:
        return e

def by_regex(df,items, axis):
    try:
        df = df.filter(regex=items, axis=axis)
        return df
    except Exception as e:
        return e

def by_contains(df,items, axis):
    try:
        df = df.filter(like=items, axis=axis)
        return df
    except Exception as e:
        return e   