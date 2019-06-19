import sys
import json
from ast import literal_eval
import pandas as pd
import re

# Deserialize dataframe
def dataframe_from_json(json_str):
    try:
        split_str = json_str[2:-2].split("},{")
        keys = [re.search('Key:(.+?),Value',x).group(1) for x in split_str]
        values = [re.search('Value:\[(.+?)\]',x).group(1) for x in split_str]
        values = [i.split(",") for i in values]
        python_dictionary = dict(zip(keys,values))
        dataframe = pd.DataFrame(python_dictionary)
        dataframe = dataframe.reset_index(drop=True)
        return dataframe
    except Exception as e:
        return e