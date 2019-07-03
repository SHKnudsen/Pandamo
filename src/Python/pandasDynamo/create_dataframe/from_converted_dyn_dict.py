import sys
import json
from ast import literal_eval
import pandas as pd
import re

# Create Dataframes
def from_converted_dyn_dict(data_string):
    try:
        split_str = data_string
        keys = re.search('Keys:(.+?),Values',split_str).group(1)[1:-1].split(',')
        values = re.search('Values:(.+?),Count',split_str).group(1)[2:-2].split("],[")
        values = [i.split(",") for i in values]
        python_dictionary = dict(zip(keys,values))
        dataframe = pd.DataFrame(python_dictionary)
        return dataframe
    except Exception as e:
        return e

df = from_converted_dyn_dict(sys.argv[1])
print(df)