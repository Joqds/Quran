class ReadViewModel {
  // ReadViewModel.surah({required this.surahId}) {
  //   type = ReadViewType.surah;
  //   hasPrevious = surahId != 0;
  //   hasNext = surahId != 114;
  // }

  // ReadViewModel.page({required this.pageId}) {
  //   type = ReadViewType.page;
  //   hasPrevious = pageId != 0;
  //   hasNext = pageId != 604;
  // }

  // ReadViewModel.hizb({required this.hizbId}) {
  //   type = ReadViewType.hizb;
  //   hasPrevious = hizbId != 0;
  //   hasNext = hizbId != 120;
  // }

  // ReadViewModel.rub({required this.rubId}) {
  //   type = ReadViewType.rub;
  //   hasPrevious = rubId != 0;
  //   hasNext = rubId != 240;
  // }

  // ReadViewModel.joz({required this.jozId}) {
  //   type = ReadViewType.joz;
  //   hasPrevious = jozId != 0;
  //   hasNext = jozId != 30;
  // }

  ReadViewModel.type({required this.type, required this.id}) {
    hasPrevious = id != 0;

    switch (type) {
      case ReadViewType.surah:
        hasNext = id != 114;
        break;
      case ReadViewType.page:
        hasNext = id != 604;
        break;
      case ReadViewType.joz:
        hasNext = id != 30;
        break;
      case ReadViewType.hizb:
        hasNext = id != 120;
        break;
      case ReadViewType.rub:
        hasNext = id != 240;
        break;
      default:
        break;
    }
  }

  bool? hasPrevious;
  bool? hasNext;
  int id;
  ReadViewType? type;
}

enum ReadViewType { surah, page, joz, hizb, rub }
