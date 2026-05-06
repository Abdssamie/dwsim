#!/usr/bin/env python3
import re
import sys

files = [
    "ActivityCoefficientBase.vb",
    "CAPEOPENSocket.vb",
    "CoolPropIncompressibleMixture.vb",
    "CoolPropIncompressiblePure.vb",
    "ElectrolyteIdeal.vb",
    "LIQUAC2PropertyPackage.vb",
    "LeeKeslerPlocker.vb",
    "NRTL.vb",
    "UNIQUAC.vb",
    "WilsonPropertyPackage.vb",
    "PengRobinsonStryjekVera2.vb",
    "PengRobinsonStryjekVera2VL.vb",
]

base = "/home/abdssamie/Projects/dwsim/DWSIM.Thermodynamics/PropertyPackages/"

for fname in files:
    path = base + fname
    with open(path, "r", encoding="utf-8-sig") as fp:
        content = fp.read()

    # Replace body of GetEditingForm() with headless stub
    pattern = (
        r"(Public Overrides Function GetEditingForm\(\) As Object)"
        r".*?"
        r"(\n\s{4,8}End Function)"
    )
    stub = (
        r"\1\n\n            ' TODO: [MIGRATION] UI editing form not available in headless mode.\n"
        r"            Return Nothing\n\2"
    )
    new_content = re.sub(pattern, stub, content, flags=re.DOTALL)
    if new_content != content:
        with open(path, "w", encoding="utf-8") as fp:
            fp.write(new_content)
        print(f"Fixed: {fname}")
    else:
        print(f"No match: {fname}")
