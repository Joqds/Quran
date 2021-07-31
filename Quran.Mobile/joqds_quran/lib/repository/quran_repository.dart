import 'dart:io';

import 'package:joqds_quran/api/quran.swagger.dart';
import 'package:localstore/localstore.dart';

class QuranRepository {
  final String surah = "surah";
  final String ayah = "ayah";

  Future<List<SurahDto>> getSovar() async {
    var api = Quran.create();
    var sovar = await api.quranGetSurahList();
    if (sovar.isSuccessful) {
      return sovar.body!;
    }
    return List.empty();
  }

  Future<List<AyahDto>> getAyatBySurah(int surahId) async {
    var api = Quran.create();
    var sovar = await api.quranGetAyatBySurah(surahId: surahId);
    if (sovar.isSuccessful) {
      return sovar.body!.ayat!;
    }
    return List.empty();
  }

  Future<List<AyahDto>> getAyatByJoz(int jozId) async {
    var api = Quran.create();
    var sovar = await api.quranGetAyatByJoz(jozId: jozId);
    if (sovar.isSuccessful) {
      return sovar.body!.ayat!;
    }
    return List.empty();
  }

  Future<List<AyahDto>> getAyatByRub(int rubId) async {
    var api = Quran.create();
    var sovar = await api.quranGetAyatByRub(rubId: rubId);
    if (sovar.isSuccessful) {
      return sovar.body!.ayat!;
    }
    return List.empty();
  }

  // Stream<int> fetchFromServer() async* {
  //   yield 0;
  //   await _fetchSurah();
  //   yield 1;
  //   for (var i = 1; i <= 30; i++) {
  //     await _fetchJoz(i);
  //     yield i + 1;
  //   }
  // }

  // Future _fetchSurah() async {
  //   var api = Quran.create();
  //   var sovar = await api.quranGetSurahList();
  //   if (!sovar.isSuccessful) {
  //     throw const HttpException("اتصال به اینترنت نیاز است");
  //   }
  //   var db = Localstore.instance;
  //   var dbSovar = await db.collection(surah).get();
  //   if (dbSovar != null) {
  //     for (var element in sovar.body!) {
  //       dbSovar.putIfAbsent(element.id.toString(), () => element.toJson());
  //     }
  //   }
  // }

  // Future _fetchJoz(int jozId) async {
  //   var api = Quran.create();
  //   var ayat = await api.quranGetAyatByJoz(jozId: jozId);
  //   if (!ayat.isSuccessful) {
  //     throw const HttpException("اتصال به اینترنت نیاز است");
  //   }
  //   var db = Localstore.instance;
  //   var dbAyat = await db.collection(ayah).get();
  //   if (dbAyat != null) {
  //     for (var element in ayat.body!.ayat!) {
  //       dbAyat.putIfAbsent(element.id.toString(), () => element.toJson());
  //     }
  //   }
  // }
}
