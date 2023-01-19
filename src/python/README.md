# Requirements
* Python 3.x

# Setup & Build
```
python3 -mvenv venv
source venv/bin/activate
pip install -r requirements.txt
```

# Running sample
```
python main.py
```

# Additional Notes

* This sample is based upon the [PyJWT](https://pyjwt.readthedocs.io/en/stable/index.html) liberary and demonstrates the use of `HS256` algorithm.
* In case you plan to encode tokens using RSA or ECDSA, you will need to install cryptography lib. You may refer to [PyJWT's documentation](https://pyjwt.readthedocs.io/en/stable/installation.html#cryptographic-dependencies-optional) for more information.