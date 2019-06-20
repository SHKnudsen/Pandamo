import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os

def filter_dataframe(df,items, axis):
    try:
        df = df.filter(items=items, axis=axis)
        return df
    except Exception as e:
        return e